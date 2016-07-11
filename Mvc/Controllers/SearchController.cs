using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfacies.Services;

namespace Mvc.Controllers
{
    public class SearchController : Controller
    {
        private readonly IProfileService profileService;

        public SearchController(IProfileService profileService)
        {
            this.profileService = profileService;
        }
        // GET: Search
        public ActionResult Index(string term)
        {

            return View();
        }
    }
}