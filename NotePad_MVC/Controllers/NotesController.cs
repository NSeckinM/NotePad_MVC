using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotePad_MVC.Data;
using NotePad_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NotePad_MVC.Controllers
{
    [Authorize]//notlar kontrolerına giriş yapmayan birinin girmesini engelliyor.
    public class NotesController : Controller
    {
        private readonly ApplicationDbContext db;

        public NotesController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Yeni()
        {
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult Yeni(YeniViewModel vm)
        {

            if (ModelState.IsValid)
            {
                Note note = new Note();
                note.Title = vm.Title;
                note.Content = vm.Content;
                note.AuthorId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                db.Notes.Add(note);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
                
            }

            return View();
        }


        public IActionResult Sil(int id)
        {

            return View(db.Notes.Find(id));
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Sil")]
        public IActionResult Silinen(int id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Note note = db.Notes.Where(x => x.Id == id && x.AuthorId == userId).FirstOrDefault();
            db.Notes.Remove(note);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");

        }


    }
}
