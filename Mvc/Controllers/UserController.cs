using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Interfacies.Services;
using Mvc.Infrastructure.Mappers;
using Mvc.Models;
using Mvc.Providers;
namespace Mvc.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IProfileService profileService;
        private readonly CustomMembershipProvider provider;
        public UserController(IUserService service, CustomMembershipProvider provider, IProfileService profileService)
        {
            this.userService = service;
            this.provider = provider;
            this.profileService = profileService;
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var membershipUser = provider.CreateUser(registerViewModel);
                if (membershipUser != null)
                {
                    MailAddress from = new MailAddress("FinalTask@mail.ru", "Web Registration");
                    MailAddress to = new MailAddress(membershipUser.Email);
                    MailMessage m = new MailMessage(from, to)
                    {
                        Subject = "Email confirmation",
                        Body = string.Format("To complete the registration please go to the link:" +
                                             "<a href=\"{0}\" title=\"Confirm registration\">{0}</a>",
                        Url.Action("ConfirmEmail", "User",
                                new {token = membershipUser.UserName, email = membershipUser.Email}, Request.Url.Scheme)),
                        IsBodyHtml = true
                    };
                    SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.mail.ru", 25)
                    {
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new System.Net.NetworkCredential("FinalTask@mail.ru", "5501mk123"),
                        EnableSsl = true,
                        Timeout = 5000
                    };
                    try
                    {
                        smtp.Send(m);
                    }
                    catch (Exception e)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    
                    FormsAuthentication.SetAuthCookie(registerViewModel.Email, false);
                    return RedirectToAction("Confirm", "User", new { Email = membershipUser.Email });
                }
                ModelState.AddModelError("", "User with the same email has registred yet");
            }
            return View(registerViewModel);
        }

        public ActionResult Login(string returnUrl)
        {
            var type = HttpContext.User.GetType();
            var iden = HttpContext.User.Identity.GetType();
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogOnViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (provider.ValidateUser(viewModel.Email, viewModel.Password))
                //Проверяет учетные данные пользователя и управляет параметрами пользователей
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Email, viewModel.RememberMe);
                    //Управляет службами проверки подлинности с помощью форм для веб-приложений
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password.");
                }
            }
            return View(viewModel);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        //GET-запрос к методу Delete несет потенциальную уязвимость!
        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            UserEntity user = userService.GetUserEntity(id);
            if (user == null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            return View();
            //return View(user.ToMvcUser());
        }

        //Post/Redirect/Get (PRG) — модель поведения веб-приложений, используемая
        //разработчиками для защиты от повторной отправки данных веб-форм
        //(Double Submit Problem)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(UserEntity user)
        {
            userService.DeleteUser(user);
            return RedirectToAction("Index", "Home");
        }

        [ChildActionOnly]
        public ActionResult LoginPartial()
        {
            return PartialView("_LoginPartial");
        }

        public string Confirm(string email)
        {
            return "On " + email + " you will be sent further instructions to complete the registration";
        }
        public ActionResult ConfirmEmail(string token, string email)
        {

            if (!provider.ConfirmEmail(id: Int32.Parse(token), email: email))
            {
                return RedirectToAction("Confirm", "User", new { Email = email });
            }
            else
            {
                return RedirectToAction("Confirm", "User", new { Email = "" });
            }
        }
        //etc.
    }
}