using CMS_Dashboard_v1.Models;
using CMS_Dashboard_v1.Models.ModelForm;
using CMS_Dashboard_v1.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CMS_Dashboard_v1.Areas.Content.Controllers
{
    [Area("Content")]
    [Authorize]
    public class HomeController : Controller
    {
        GlobalListApi _globallist = new GlobalListApi();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("Home/Index")]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var content = await _globallist.GetListContent();
            var section = await _globallist.GetListSection();
            var menu = await _globallist.GetListMenu();
            var Model = new Article();

            ViewData["TotalMenu"] = menu.Count;
            ViewData["TotalSection"] = section.Count;
            ViewData["TotalContent"] = content.Count;


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}