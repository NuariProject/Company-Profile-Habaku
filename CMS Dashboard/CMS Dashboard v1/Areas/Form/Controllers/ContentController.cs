using CMS_Dashboard_v1.Models;
using CMS_Dashboard_v1.Models.ModelForm;
using CMS_Dashboard_v1.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.Net;

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

        public async Task<IActionResult> Index(ContentModel Model)
        {
            var content = await _globallist.GetListContent();
            var section = await _globallist.GetListSection();
            var menu = await _globallist.GetListMenu();

            try
            {
                if (content != null && section != null && menu != null)
                {
                    Model.List = (from a in content.Where(ss => ss.status).ToList()
                                  join b in section.Where(ss => ss.status && ss.section_id != 12).ToList() on a.section_id equals b.section_id
                                  join c in menu.Where(ss => ss.status).ToList() on b.menu_id equals c.menu_id
                                  select new ContentViewModel
                                  {
                                      content_id = a.content_id,
                                      Menu = c.menu_name,
                                      Section = b.section_name,
                                      header = a.header,
                                      title = a.title,
                                      description = a.description,
                                      imageurl = a.image,
                                      url = a.url
                                  }).ToList();
                }
                else
                {
                    NotifMessage("error", "Gagal load data menu");
                }
            }
            catch (Exception e)
            {
                NotifMessage("error", e.Message.ToString());
            }

            return View(Model);
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

        [Route("Content/Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            var model = new ContentViewModel();
            try
            {
                model = await FindData(id);
            }
            catch (Exception e)
            {
                NotifMessage("error", " Error : " + e.Message.ToString());
                return RedirectToAction("Index", "Content");
            }
            return View(model);
        }

        [Route("Content/Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            var model = new ContentViewModel();
            try
            {
                model = await FindData(id);
            }
            catch (Exception e)
            {
                NotifMessage("error", " Error : " + e.Message.ToString());
                return RedirectToAction("Index", "Content");
            }
            return View(model);
        }

        private async Task<ContentViewModel> FindData(int? id)
        {
            var val = new ContentViewModel();
            try
            {
                var content = await _globallist.GetListContent();
                var section = await _globallist.GetListSection();
                var menu = await _globallist.GetListMenu();

                val = (from a in content.Where(ss => ss.status && ss.content_id == id).ToList()
                        join b in section.Where(ss => ss.status).ToList()
                        on a.section_id equals b.section_id
                        join c in menu.Where(ss => ss.status).ToList()
                        on b.menu_id equals c.menu_id
                        select new ContentViewModel
                        {
                            content_id = a.content_id,
                            section_id = b.section_id,
                            menu_id = Convert.ToInt16(c.menu_id),
                            Menu = c.menu_name,
                            Section = b.section_name,
                            header = a.header,
                            title = a.title,
                            imageurl = a.image,
                            url = a.url,
                            description = a.description
                        }).FirstOrDefault();
            }
            catch (Exception e)
            {
                NotifMessage("error", " Error : " + e.Message.ToString());
            }

            return val;
        }

        [HttpGet]
        [Route("Content/GetValidasiContent")]
        public async Task<IActionResult> GetValidasiContent(int idmenu, int idsection)
        {
            var content = await _globallist.GetListContent();
            var section = await _globallist.GetListSection();

            // Jumlah Content disection yang dipilih
            // Mengecek Content yang sudah diinput pada section ini
            // Init
            var contentlist = content.Where(ss => ss.status).ToList();

            var val = (from a in contentlist
                       join b in section.Where(ss => ss.status && ss.section_id == idsection).ToList()
                       on a.section_id equals b.section_id
                       select new
                       {
                           approve = b.section_approve,
                           header =  contentlist.Count(ss => ss.section_id == idsection) > 0  ? a.header : ""
                       }).FirstOrDefault();

            return Json(val != null ? val : new { approve = 0, header = ""});
        }

        [HttpPost]
        [Route("Content/Create")]
        public async Task<IActionResult> Create(ContentViewModel model)
        {
            var content = await _globallist.GetListContent();
            var section = await _globallist.GetListSection();
            var batasContent = section.Where(ss => ss.status && ss.section_id == model.section_id).FirstOrDefault();
            var totalContent = content.Where(ss => ss.status && ss.section_id == model.section_id).ToList();
            
            if (totalContent.Count() >= batasContent.section_approve)
            {
                ModelState.AddModelError("section_id", "Total content anda di section ini : " + totalContent.Count() + " Content || " +
                    "Batas content di section ini : "+batasContent.section_approve+" Content");
            }
            try
            {
                if (ModelState.IsValid || model.Photos == null)
                {
                    if (model.Photos != null)
                    {
                        string path = "cover/img/";
                        path += Guid.NewGuid().ToString() + "_" + model.Photos.FileName;
                        model.imageurl = "/" + path;
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

                    if (BaseResponse.code == Convert.ToInt16(HttpStatusCode.OK))
                    {
                        NotifMessage("success", "Content berhasil disimpan");
                        return RedirectToAction("Index", "Content");
                    }
                    else
                    {
                        NotifMessage("error", " Data gagal disimpan");
                    }
                }
            }
            catch (Exception e)
            {
                NotifMessage("error", " Error : " + e.Message.ToString());
            }

            return View(model);
        }

        [HttpPost]
        [Route("Content/Edit")]
        public async Task<IActionResult> Edit(ContentViewModel model)
        {
            try
            {
                if (ModelState.IsValid || model.Photos == null)
                {
                    if (model.Photos != null)
                    {
                        string path = "cover/img/";
                        path += Guid.NewGuid().ToString() + "_" + model.Photos.FileName;
                        model.imageurl = "/" + path;
                        string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, path);

                        await model.Photos.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
                    }

                    var obj = new PutContentModel
                    {
                        content_id = model.content_id,
                        section_id = model.section_id,
                        header = model.header,
                        title = model.title,
                        description = model.description,
                        image = model.imageurl,
                        url = model.url,
                        status = true,
                        modified_by = HttpContext.Session.GetString("Username")
                    };

                    MasterDataService _masterDataService = new MasterDataService();
                    var baseadd = _configuration.GetValue<string>("Api-CMS:BaseAddress");
                    var enpoint = _configuration.GetValue<string>("Api-CMS:Content");
                    var data = await _masterDataService.PutAsync(baseadd + enpoint, obj);
                    string jsonString = await data.Content.ReadAsStringAsync();
                    var BaseResponse = JsonConvert.DeserializeObject<BaseResponse<List<ContentModel>>>(jsonString);

                    if (BaseResponse.code == Convert.ToInt16(HttpStatusCode.OK))
                    {
                        NotifMessage("success", "Content berhasil diupdate");
                        return RedirectToAction("Index", "Content");
                    }
                    else
                    {
                        NotifMessage("error", " Data gagal diupdate");
                    }
                }
            }
            catch (Exception e)
            {
                NotifMessage("error", " Error : " + e.Message.ToString());
            }

            return View(model);
        }

        [HttpPost]
        [Route("Content/Delete")]
        public async Task<IActionResult> Delete(ContentViewModel model)
        {
            try
            {
                if (ModelState.IsValid || model.Photos == null)
                {
                    if (model.Photos != null)
                    {
                        string path = "cover/img/";
                        path += Guid.NewGuid().ToString() + "_" + model.Photos.FileName;
                        model.imageurl = "/" + path;
                        string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, path);

                        await model.Photos.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
                    }

                    var obj = new PutContentModel
                    {
                        content_id = model.content_id,
                        section_id = model.section_id,
                        header = model.header,
                        title = model.title,
                        description = model.description,
                        image = model.imageurl,
                        url = model.url,
                        status = false,
                        modified_by = HttpContext.Session.GetString("Username")
                    };

                    MasterDataService _masterDataService = new MasterDataService();
                    var baseadd = _configuration.GetValue<string>("Api-CMS:BaseAddress");
                    var enpoint = _configuration.GetValue<string>("Api-CMS:Content");
                    var data = await _masterDataService.PutAsync(baseadd + enpoint, obj);
                    string jsonString = await data.Content.ReadAsStringAsync();
                    var BaseResponse = JsonConvert.DeserializeObject<BaseResponse<List<ContentModel>>>(jsonString);

                    if (BaseResponse.code == Convert.ToInt16(HttpStatusCode.OK))
                    {
                        NotifMessage("success", "Content berhasil dihapus");
                        return RedirectToAction("Index", "Content");
                    }
                    else
                    {
                        NotifMessage("error", " Data gagal dihapus");
                    }
                }
            }
            catch (Exception e)
            {
                NotifMessage("error", " Error : " + e.Message.ToString());
            }

            return View(model);
        }

    }
}
