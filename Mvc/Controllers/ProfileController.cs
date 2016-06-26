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

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("NotFound", "Home");
            var profile = profileService.GetProfileByUserId(id.Value);
            if(profile == null)
                return RedirectToAction("NotFound", "Home");
            if (User.Identity.Name != profile.Email)
                return RedirectToAction("Index", id);
            return View(profile);
        }


        //[HttpPost]
        //public JsonResult Upload()
        //{
        //    bool flag = false;
        //    List<ImageUploadResult> UploadResultlist = new List<ImageUploadResult>();
        //    string fileName = "";
        //    Account account = new Account(Resources.GlobalResources.cloudName, Resources.GlobalResources.cloudApi, Resources.GlobalResources.cloudApiSecret);
        //    CloudinaryDotNet.Cloudinary cloudinary = new Cloudinary(account);
        //    ImageUploadResult uploadResultImg = new ImageUploadResult();
        //    ImageUploadResult uploadResultDemotivator = new ImageUploadResult();
        //    ImageUploadParams uploadParamsImg = new ImageUploadParams();
        //    ImageUploadParams uploadParamsDemotivator = new ImageUploadParams();
        //    foreach (string file in Request.Files)
        //    {
        //        var upload = Request.Files[file];
        //        if (upload != null)
        //        {
        //            // получаем имя файла
        //            fileName = System.IO.Path.GetFileName(upload.FileName);
        //            // сохраняем файл в папку Files в проекте
        //            upload.SaveAs(Server.MapPath("~/Files/" + fileName));
        //            uploadParamsImg = new ImageUploadParams()
        //            {
        //                File = new FileDescription(Server.MapPath("~/Files/" + fileName)),
        //                PublicId = User.Identity.Name + fileName + DateTime.Now.Millisecond.ToString(),
        //            };
        //        }
        //    }
        //    foreach (string file in Request.Form)
        //    {
        //        var upload = Request.Form[file];
        //        if (upload != null)
        //        {
        //            if (upload.Contains("data:image/png;base64"))
        //            {
        //                string x = upload.Replace("data:image/png;base64,", "");
        //                byte[] imageBytes = Convert.FromBase64String(x);
        //                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);


        //                ms.Write(imageBytes, 0, imageBytes.Length);
        //                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
        //                image.Save(Server.MapPath("~/Files/img.png"), System.Drawing.Imaging.ImageFormat.Png);

        //                uploadParamsDemotivator = new ImageUploadParams()
        //                {
        //                    File = new FileDescription(Server.MapPath("~/Files/img.png")),
        //                    PublicId = User.Identity.Name + fileName + "demotivator" + DateTime.Now.Millisecond.ToString()
        //                };
        //            }
        //            else
        //            {
        //                flag = true;
        //                uploadParamsImg = new ImageUploadParams()
        //                {
        //                    File = new FileDescription(upload),
        //                    PublicId = User.Identity.Name + fileName + DateTime.Now.Millisecond.ToString()
        //                };
        //            }
        //        }
        //    }

        //    uploadResultImg = cloudinary.Upload(uploadParamsImg);
        //    UploadResultlist.Add(uploadResultImg);
        //    uploadResultDemotivator = cloudinary.Upload(uploadParamsDemotivator);
        //    UploadResultlist.Add(uploadResultDemotivator);
        //    if (!flag) System.IO.File.Delete(Server.MapPath("~/Files/" + fileName));
        //    System.IO.File.Delete(Server.MapPath("~/Files/img.png"));
        //    return Json(UploadResultlist, JsonRequestBehavior.AllowGet);
        //}
    }
}