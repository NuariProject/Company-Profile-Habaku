using Microsoft.AspNetCore.Mvc;

namespace CMS_Dashboard_v1.Controllers
{
    public class LoginController : Controller
    {

        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Auth()
        {
            return RedirectToAction("Index","Home");
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Index","Login");
        }

    }
}
