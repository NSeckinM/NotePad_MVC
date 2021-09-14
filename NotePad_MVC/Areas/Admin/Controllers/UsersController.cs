using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotePad_MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotePad_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext db;

        public UsersController(ApplicationDbContext db)
        {
            this.db = db;
        }


        public IActionResult Index()
        {
            return View(db.Users.ToList());
        }

        [HttpPost]
        public IActionResult ChangeStatus(string userId, bool active)
        {

            ApplicationUser user = db.Users.Find(userId);
            if (user == null)
            {
                return NotFound();
            }
            user.LockoutEnabled = true;
            user.LockoutEnd = active ? null : DateTimeOffset.MaxValue;
            db.SaveChanges();


            return Ok();
        }
    }
}
