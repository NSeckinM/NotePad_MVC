using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotePad_MVC.Data
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime RegistrationTime { get; set; } = DateTime.Now;
        public List<Note> Notes { get; set; }


    }
}
