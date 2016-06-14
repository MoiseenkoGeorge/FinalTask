using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;
using MvcPL.Providers;

namespace MvcPL.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {

        private readonly IUserService service;
        private readonly CustomMembershipProvider provider;
        public HomeController(IUserService service,CustomMembershipProvider provider)
        {
            this.service = service;
            this.provider = provider;
        }

        public ActionResult Index()
        {
            return View(provider.GetAllUsers());
        }

        public ActionResult About()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                ViewBag.AuthType = User.Identity.AuthenticationType;
            }
            ViewBag.Login = User.Identity.Name;
            ViewBag.IsAdminInRole = User.IsInRole("Administrator") ?
                "You have administrator rights." : "You do not have administrator rights.";

            return View();
        }

        //[Authorize(Roles = "Administrator")]
        //public ActionResult Edit()
        //{
        //    var model = _repository.GetAllUsers().Select(u => new UserViewModel
        //    {
        //        Email = u.Email,
        //        CreationDate = u.CreationDate,
        //        Role = u.Role.Name
        //    });

        //    return System.Web.UI.WebControls.View(model);
        //}
    //}

        public ActionResult ProfileEdit()
        {
            throw new NotImplementedException();
        }
    }
}
