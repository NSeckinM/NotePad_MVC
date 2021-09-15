using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotePad_MVC.Data;
using NotePad_MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NotePad_MVC.Controllers
{
    [Authorize]
    public class ProfilePhotoController : Controller
    {
        private readonly IWebHostEnvironment env;
        private readonly ApplicationDbContext db;

        public ProfilePhotoController(IWebHostEnvironment env, ApplicationDbContext db)
        {
           
            this.env = env;
            this.db = db;
        }

        public IActionResult Index( string result)
        {
            var vm = new ProfilePhotoViewModel()
            {
                Yuklendi = result == "Uploaded",
                Foto = db.Users.Find(User.FindFirstValue(ClaimTypes.NameIdentifier)).Photo
            };

            return View(vm);
        }

        [HttpPost ,ValidateAntiForgeryToken]
        public IActionResult Index(IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
                ModelState.AddModelError("photo", "yüklenecek resim dosyası bulunamadı");

            else if (!photo.ContentType.StartsWith("image/"))
                ModelState.AddModelError("photo", "Lütfen bir resim dosyası seçin");

            //izin verilen en büyük dosya boyutu 1mb
            else if (photo.Length > 1 * 1000 * 1000)
                ModelState.AddModelError("photo", "maksimum dosya boyutu 1 mb'dır");


            if (ModelState.IsValid)
            {
                string uzanti = Path.GetExtension(photo.FileName);
                string yeniDosyaAd = Guid.NewGuid() + uzanti; // doaya adını diğer gelecek fotolarla aynı olup karışmaması için uniq yaptık.
                string kayityolu = Path.Combine(env.WebRootPath, "uploads", yeniDosyaAd);

                using (FileStream fs = System.IO.File.Create(kayityolu))
                {
                    photo.CopyTo(fs);
                }

                ApplicationUser user = db.Users.Find(User.FindFirstValue(ClaimTypes.NameIdentifier));
                DeletePhotoFile(user.Photo);
                user.Photo = yeniDosyaAd;
                db.SaveChanges();
                return RedirectToAction("Index", new {result = "Uploaded" });

            }
            var vm = new ProfilePhotoViewModel()
            {
                Foto = db.Users.Find(User.FindFirstValue(ClaimTypes.NameIdentifier)).Photo
            };
            return View(vm);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeletePhoto()
        {
            ApplicationUser user = db.Users.Find(User.FindFirstValue(ClaimTypes.NameIdentifier));
            DeletePhotoFile(user.Photo);
            user.Photo = null;
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        private void DeletePhotoFile(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                var photoPath = Path.Combine(env.WebRootPath, "uploads", fileName);
                if (System.IO.File.Exists(photoPath))
                {
                    System.IO.File.Delete(photoPath);
                }
            }
        }
    }
}
