using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotePad_MVC.Models
{
    public class YeniViewModel
    {
        [Required]
        public string Title { get; set; }

        public string Content { get; set; }

    }
}
