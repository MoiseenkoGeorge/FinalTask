using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfacies.Services;
using Mvc.Infrastructure.Mappers;
using Mvc.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

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
        [HttpGet]
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("NotFound", "Home");
            var profile = profileService.GetProfileByUserId(id.Value);
            if(profile == null)
                return RedirectToAction("NotFound", "Home");
            if (User.Identity.Name != profile.Email)
                return RedirectToAction("Index", id);
            return View(profile.ToProfileViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(ProfileViewModel profileViewModel)
        {
            profileViewModel.UserId = profileViewModel.Id;
            profileService.UpdateProfile(profileViewModel.ToProfileEntity());
            return View(profileViewModel);
        }

        [HttpPost]
        public JsonResult Upload()
        {
            bool flag = false;
            List<ImageUploadResult> UploadResultlist = new List<ImageUploadResult>();
            string fileName = "";
            Account account = new Account("djb7hr8nk", "981823513498862", "ortnIAykexsGNcYTGiMTNjIarvo");
            CloudinaryDotNet.Cloudinary cloudinary = new Cloudinary(account);
            ImageUploadResult uploadResultImg = new ImageUploadResult();
            ImageUploadParams uploadParamsImg = new ImageUploadParams();
            foreach (string file in Request.Files)
            {
                var upload = Request.Files[file];
                if (upload != null)
                {
                    // получаем имя файла
                    fileName = System.IO.Path.GetFileName(upload.FileName);
                    // сохраняем файл в папку Files в проекте
                    upload.SaveAs(Server.MapPath("~/Files/" + fileName));
                    uploadParamsImg = new ImageUploadParams()
                    {
                        File = new FileDescription(Server.MapPath("~/Files/" + fileName)),
                        PublicId = User.Identity.Name + fileName + DateTime.Now.Millisecond.ToString(),
                    };
                }
            }

            uploadResultImg = cloudinary.Upload(uploadParamsImg);
            UploadResultlist.Add(uploadResultImg);
            if (!flag) System.IO.File.Delete(Server.MapPath("~/Files/" + fileName));
            return Json(UploadResultlist, JsonRequestBehavior.AllowGet);
        }
    }
}