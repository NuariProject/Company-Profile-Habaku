using CMS_Dashboard_v1.Models.ModelForm;
using CMS_Dashboard_v1.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS_Dashboard_v1.Areas.Form.Controllers
{
    [Area("Form")]
    [Authorize]
    public class ContentController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ContentController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        public void NotifMessage(string status, string messages)
        {
            TempData[status] = status;
            TempData["message"] = messages;
        }

        [HttpGet]
        [Route("Content/GetSectionList")]
        public async Task<IActionResult> GetSectionList(string id)
        {
            var general = new GeneralService();
            var dropdownData = await general.DropdownSection(Convert.ToInt16(id));
            return Json(dropdownData);
        }

        [Route("Content/Create")]
        public IActionResult Create()
        {
            var model = new ContentViewModel();
            return View(model);
        }

        [HttpPost]
        [Route("Content/Create")]
        public async Task<IActionResult> Create(ContentViewModel model)
        {
            if(model.Photos != null)
            {
                string path = "cover/img/";
                path += Guid.NewGuid().ToString()+"_"+model.Photos.FileName;
                model.imageurl = path;
                string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, path);

                await model.Photos.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
            }




            return View(model);
        }
        }
}
