using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfacies.Services;
using Mvc.Infrastructure.Mappers;

namespace Mvc.Controllers
{
    [AllowAnonymous]
    public class ProfileController : Controller
    {
        private readonly IProfileService profileService;

        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        //
        // GET: /Profile/
        [HttpGet]
        public ActionResult Index(int id = 0)
        {
            var profile = profileService.GetProfileByUserId(id);
            if (profile == null)
                return View("Error");
            return View(profile.ToProfileViewModel());
        }

    }
}