using CMS_Dashboard_v1.Models;
using CMS_Dashboard_v1.Models.ModelForm;
using CMS_Dashboard_v1.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Net;
using System.Reflection;

namespace CMS_Dashboard_v1.Areas.Form.Controllers
{
    [Area("Form")]
    [Authorize]
    public class ArtikelController : Controller
    {
        readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        GlobalListApi _globallist = new GlobalListApi();
        public ArtikelController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
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
                    Model.ListArtikel = (from a in content.Where(ss => ss.status).ToList()
                                         join b in section.Where(ss => ss.status && ss.section_id == 12).ToList() on a.section_id equals b.section_id
                                         join c in menu.Where(ss => ss.status).ToList() on b.menu_id equals c.menu_id
                                         select new ArtikelViewModel
                                         {
                                             content_id = a.content_id, // IdArtikel
                                             header = a.header, // Judul
                                             title = a.title, // Category
                                             publish = a.created_at.ToString("dd MMMM yyyy", new CultureInfo("id-ID")), // Tanggal Artikel Dibuat
                                             description = a.description, // Description
                                             imageurl = a.image, // Sampul
                                         }).ToList();
                }
                else
                {
                    NotifMessage("error", "Gagal load data Artikel");
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

        [Route("Artikel/Create")]
        public async Task<IActionResult> Create()
        {
            var model = new ArtikelViewModel();
            var menu = await _globallist.GetListMenu();
            var section = await _globallist.GetListSection();

            var def = (from a in menu.Where(ss => ss.menu_name.Contains("Artikel"))
                       join b in section on a.menu_id equals b.menu_id
                       select b).OrderByDescending(ss => ss.section_number).FirstOrDefault();
            if (def != null)
            {
                model.section_id = def.section_id;
            }

            return View(model);
        }

        [HttpPost]
        [Route("Artikel/Create")]
        public async Task<IActionResult> Create(ArtikelViewModel model)
        {
            var content = await _globallist.GetListContent();
            var section = await _globallist.GetListSection();
            var batasContent = section.Where(ss => ss.status && ss.section_id == model.section_id).FirstOrDefault();
            var totalContent = content.Where(ss => ss.status && ss.section_id == model.section_id).ToList();

            ModelState.Remove("Publish");
            if (totalContent.Count() >= batasContent.section_approve)
            {
                ModelState.AddModelError("section_id", "Batas Maksimal");

                NotifMessage("error", "Total Artikel anda : " + totalContent.Count() + " Artikel || " +
                    "Batas Tulis Artikel : " + batasContent.section_approve + " Artikel");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Photos != null)
                    {
                        string path = "cover/Artikel/";
                        path += Guid.NewGuid().ToString() + "_" + model.Photos.FileName;
                        model.imageurl = "/" + path;
                        string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, path);

                        await model.Photos.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
                    }

                    var obj = new InsertContentModel
                    {
                        section_id = model.section_id,
                        header = model.header, // Judul Artikel
                        title = model.title, // Category Artikel
                        description = model.description, // Body Artikel
                        image = model.imageurl, // Sampul Artikel
                        url = "",
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
                        NotifMessage("success", "Arikel berhasil disimpan");
                        return RedirectToAction("Index", "Artikel");
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


        [Route("Artikel/Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            var model = new ArtikelViewModel();
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

        [Route("Artikel/Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            var model = new ArtikelViewModel();
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

        private async Task<ArtikelViewModel> FindData(int? id)
        {
            var val = new ArtikelViewModel();
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
                       select new ArtikelViewModel
                       {
                           content_id = a.content_id,
                           section_id = b.section_id,
                           menu_id = Convert.ToInt16(c.menu_id),
                           header = a.header,
                           title = a.title,
                           imageurl = a.image,
                           description = a.description
                       }).FirstOrDefault();
            }
            catch (Exception e)
            {
                NotifMessage("error", " Error : " + e.Message.ToString());
            }

            return val;
        }


        [HttpPost]
        [Route("Artikel/Edit")]
        public async Task<IActionResult> Edit(ArtikelViewModel model)
        {
            try
            {
                var content = await _globallist.GetListContent();
                var section = await _globallist.GetListSection();
                var batasContent = section.Where(ss => ss.status && ss.section_id == model.section_id).FirstOrDefault();
                var totalContent = content.Where(ss => ss.status && ss.section_id == model.section_id).ToList();

                ModelState.Remove("Publish");
                ModelState.Remove("Photos");

                if (ModelState.IsValid)
                {
                    if (model.Photos != null)
                    {
                        string path = "cover/Artikel/";
                        path += Guid.NewGuid().ToString() + "_" + model.Photos.FileName;
                        model.imageurl = "/" + path;
                        string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, path);

                        await model.Photos.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
                    }
                    else
                    {
                        model.imageurl = content.FirstOrDefault(ss => ss.status && ss.content_id == model.content_id).image;
                    }

                    var obj = new PutContentModel
                    {
                        content_id = model.content_id,
                        section_id = model.section_id,
                        header = model.header,
                        title = model.title,
                        description = model.description,
                        image = model.imageurl,
                        url = "",
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
                        NotifMessage("success", "Artikel berhasil diupdate");
                        return RedirectToAction("Index", "Artikel");
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
        [Route("Artikel/Delete")]
        public async Task<IActionResult> Delete(ArtikelViewModel model)
        {
            try
            {
                ModelState.Remove("Publish");
                ModelState.Remove("Photos");
                if (ModelState.IsValid)
                {
                    var content = await _globallist.GetListContent();
                    if (model.Photos != null)
                    {
                        string path = "cover/Artikel/";
                        path += Guid.NewGuid().ToString() + "_" + model.Photos.FileName;
                        model.imageurl = "/" + path;
                        string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, path);

                        await model.Photos.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
                    }
                    else
                    {
                        model.imageurl = content.FirstOrDefault(ss => ss.status && ss.content_id == model.content_id).image;
                    }

                    var obj = new PutContentModel
                    {
                        content_id = model.content_id,
                        section_id = model.section_id,
                        header = model.header,
                        title = model.title,
                        description = model.description,
                        image = model.imageurl,
                        url = "",
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
                        NotifMessage("success", "Artikel berhasil dihapus");
                        return RedirectToAction("Index", "Artikel");
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
