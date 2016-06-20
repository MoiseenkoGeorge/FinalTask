using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;
using MvcPL.Providers;

namespace MvcPL.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly IUserService service;
        private readonly CustomMembershipProvider provider;
        public UserController(IUserService service,CustomMembershipProvider provider)
        {
            this.service = service;
            this.provider = provider;
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
            if(ModelState.IsValid)
            {
                var membershipUser = provider.CreateUser(registerViewModel);
                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(registerViewModel.Email, false);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "User with the same email has registred yet");
            }
            return View(registerViewModel);
        }

        [AllowAnonymous]
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
            UserEntity user = service.GetUserEntity(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View();
            //return View(user.ToMvcUser());
        }

        //Post/Redirect/Get (PRG) — модель поведения веб-приложений, используемая
        //разработчиками для защиты от повторной отправки данных веб-форм
        //(Double Submit Problem)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(UserEntity user)
        {
            service.DeleteUser(user);
            return RedirectToAction("Index","Home");
        }

        [ChildActionOnly]
        public ActionResult LoginPartial()
        {
            return PartialView("_LoginPartial");
        }
        //etc.
    }
}