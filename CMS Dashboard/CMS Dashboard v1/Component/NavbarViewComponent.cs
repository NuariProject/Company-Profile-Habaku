using CMS_Dashboard_v1.Models;
using Microsoft.AspNetCore.Mvc;

namespace CMS_Dashboard_v1.Component
{
    public class NavbarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(NavbarModel Model)
        {
            Model.nama = HttpContext.Session.GetString("Username");
            Model.role = HttpContext.Session.GetString("Role");

            for (int i = 0; i < 3; i++)
            {
                Model.load.Add(new Content { name = "Hallo " + i });
            }



            return View(Model);
        }
    }
}
