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

        [Route("/produk")]
        public IActionResult Produk()
        {
            return View();
        }

        [Route("/artikel")]
        public IActionResult Artikel()
        {
            return View();
        }

        [Route("/tentangkami")]
        public IActionResult TentangKami()
        {
            return View();
        }

        [Route("/hubungikami")]
        public IActionResult HubungiKami()
        {
            return View();
        }
        [Route("/detailartikel")]
        public IActionResult DetailArtikel()
        {
            return View();
        }
    }
}
