using Microsoft.AspNetCore.Mvc;

namespace CMS_Dashboard_v1.Areas.LandingPage.Controllers
{
    [Area("LandingPage")]
    public class HabakuController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
