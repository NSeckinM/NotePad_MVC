using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotePad_MVC.Data
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime RegistrationTime { get; set; } = DateTime.Now;

        [MaxLength(255)]
        public string Photo { get; set; }
        public List<Note> Notes { get; set; }


    }
}
