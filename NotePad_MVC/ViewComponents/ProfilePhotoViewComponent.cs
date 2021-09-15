using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NotePad_MVC.Data;
using NotePad_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace NotePad_MVC.ViewComponents
{
    //https://docs.microsoft.com/tr-tr/aspnet/core/mvc/views/view-components?view=aspnetcore-5.0
    public class ProfilePhotoViewComponent: ViewComponent
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ProfilePhotoViewComponent(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync(int genislik, int yukseklik, string sinif)
        {
            var userId = ((ClaimsPrincipal)User).FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(userId);

            var vm = new ProfilePhotoComponentViewModel()
            {
                FileName = user.Photo,
                Width = genislik,
                Height = yukseklik,
                class1 = sinif
            };


            return View(vm);
        }


    }
}
