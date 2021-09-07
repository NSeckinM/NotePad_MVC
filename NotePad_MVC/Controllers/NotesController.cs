using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotePad_MVC.Data;
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
        public IActionResult Yeni(Note note)
        {

            if (ModelState.IsValid)
            {
                note.AuthorId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                db.Notes.Add(note);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
                
            }

            return View();
        }
    }
}
