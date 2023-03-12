using CMS_Dashboard_v1.Models;
using Microsoft.AspNetCore.Mvc;

namespace CMS_Dashboard_v1.Areas.Content.Controllers
{
    public class GeneratePartialViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NavbarPartial()
        {
            var navbar = new NavbarModel();

            navbar.nama = HttpContext.Session.GetString("Username");
            navbar.role = HttpContext.Session.GetString("Role");

            return PartialView("_Navbar", navbar);
        }
    }
}
