using CMS_Dashboard_v1.Models;
using CMS_Dashboard_v1.Models.ModelForm;
using CMS_Dashboard_v1.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CMS_Dashboard_v1.Areas.Form.Controllers
{
    [Area("Form")]
    [Authorize]
    public class ContentController : Controller
    {
        readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        GlobalListApi _globallist = new GlobalListApi();
        public ContentController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _configuration = configuration;
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
                model.imageurl = "/"+path;
                string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, path);

                await model.Photos.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
            }

            var obj = new InsertContentModel 
            {
                section_id = model.section_id,
                header = model.header,
                title = model.title,
                description = model.description,
                image = model.imageurl,
                url = model.url,
                status = true,
                created_by = HttpContext.Session.GetString("Username") 
            };

            MasterDataService _masterDataService = new MasterDataService();
            var baseadd = _configuration.GetValue<string>("Api-CMS:BaseAddress");
            var enpoint = _configuration.GetValue<string>("Api-CMS:Content");
            var data = await _masterDataService.PostAsync(baseadd + enpoint, obj);
            string jsonString = await data.Content.ReadAsStringAsync();
            var BaseResponse = JsonConvert.DeserializeObject<BaseResponse<List<ContentModel>>>(jsonString);




            return View(model);
        }
        }
}
