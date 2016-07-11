using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Services;
using BLL.Interfacies.Services;
using BLL.Interfacies.Entities;
using Mvc.Infrastructure.Mappers;
using Mvc.Models;
using Mvc.Providers;


namespace Mvc.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {

        private readonly CustomMembershipProvider provider;
        private readonly IProfileService profileService;
        public HomeController(CustomMembershipProvider provider,IProfileService profileService)
        {
            this.provider = provider;
            this.profileService = profileService;
        }

        public ActionResult Index()
        {
            return View(profileService.GetAllProfileEntities().Select(p => p.ToProfileViewModel()));
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
        public ActionResult NotFound()
        {
            return View("Error");
        }
    }
}