using CMS_Dashboard_v1.Models.ModelModule;
using CMS_Dashboard_v1.Models;
using CMS_Dashboard_v1.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using CMS_Dashboard_v1.Models.ModelForm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
using System.Net;
using System.Configuration;

namespace CMS_Dashboard_v1.Areas.Form.Controllers
{
    [Area("Form")]
    [Authorize]
    public class MenuController : Controller
    {
        readonly IConfiguration _configuration;
        GlobalListApi _globallist = new GlobalListApi();
        public MenuController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<IActionResult> Index(MenuModel Model)
        {
            var response =  await _globallist.GetListMenu();

            try
            {
                if (response != null)
                {
                    Model.List = (from a in response.Where(ss => ss.status)
                                  select new MenuViewModel
                                  {
                                      menu_id = Convert.ToInt16(a.menu_id),
                                      menu_name = a.menu_name,
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

        [Route("Menu/Create")]
        public IActionResult Create()
        {
            var model = new MenuViewModel();
            return View();
        }

        [HttpPost]
        [Route("Menu/Create")]
        public async Task<IActionResult> Create(MenuViewModel model)
            {
            var response = await _globallist.GetListMenu();
            if (response.Any(ss => ss.status && ss.menu_name == model.menu_name))
                ModelState.AddModelError("menu_name", "Nama menu sudah terdaftar");

            try
            {
                if (ModelState.IsValid)
                {
                    var obj = new InsertMenuModel { menu_name = model.menu_name, created_by = HttpContext.Session.GetString("Username")};

                    MasterDataService _masterDataService = new MasterDataService();
                    var baseadd = _configuration.GetValue<string>("Api-CMS:BaseAddress");
                    var enpoint = _configuration.GetValue<string>("Api-CMS:Menu");
                    var data = await _masterDataService.PostAsync(baseadd + enpoint, obj);
                    string jsonString = await data.Content.ReadAsStringAsync();
                    var BaseResponse = JsonConvert.DeserializeObject<BaseResponse<List<MenuModel>>>(jsonString);

                    if(BaseResponse.code == Convert.ToInt16(HttpStatusCode.OK))
                    {
                        NotifMessage("success", "Data berhasil disimpan");
                        return RedirectToAction("Index","Menu");
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

        public async Task<MenuViewModel> FindData(int id)
        {
            var model = new MenuViewModel();
            var response = await _globallist.GetListMenu();
            var value = response.Where(ss => ss.menu_id == id && ss.status).FirstOrDefault();

            model.menu_id = Convert.ToInt16(value.menu_id);
            model.menu_name = value.menu_name;

            return (model);
        }

        [HttpGet]
        [Route("Menu/Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await FindData(id);

            return View(model);
        }

        [HttpPost]
        [Route("Menu/Edit")]
        public async Task<IActionResult> Edit(MenuViewModel model)
        {
            var response = await _globallist.GetListMenu();
            if (response.Any(ss => ss.status && ss.menu_name == model.menu_name && ss.menu_id != model.menu_id))
                ModelState.AddModelError("menu_name", "Nama menu sudah terdaftar");

            try
            {
                if (ModelState.IsValid)
                {
                    var obj = new PutMenuModel {menu_id = model.menu_id, 
                                                menu_name = model.menu_name,
                                                status = true,
                                                modified_by = HttpContext.Session.GetString("Username") };

                    MasterDataService _masterDataService = new MasterDataService();
                    var baseadd = _configuration.GetValue<string>("Api-CMS:BaseAddress");
                    var enpoint = _configuration.GetValue<string>("Api-CMS:Menu");
                    var data = await _masterDataService.PutAsync(baseadd + enpoint, obj);
                    string jsonString = await data.Content.ReadAsStringAsync();
                    var BaseResponse = JsonConvert.DeserializeObject<BaseResponse<List<MenuModel>>>(jsonString);

                    if (BaseResponse.code == Convert.ToInt16(HttpStatusCode.OK))
                    {
                        NotifMessage("success", "Data berhasil diubah");
                        return RedirectToAction("Index", "Menu");
                    }
                    else
                    {
                        NotifMessage("error", " Data gagal diubah");
                    }
                }
            }
            catch (Exception e)
            {
                NotifMessage("error", " Error : " + e.Message.ToString());
            }

            return View(model);
        }

        [Route("Menu/Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await FindData(id);

            return View(model);
        }

        [HttpPost]
        [Route("Menu/Delete")]
        public async Task<IActionResult> Delete(MenuViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var obj = new PutMenuModel
                    {
                        menu_id = model.menu_id,
                        menu_name = model.menu_name,
                        status = false,
                        modified_by = HttpContext.Session.GetString("Username")
                    };

                    MasterDataService _masterDataService = new MasterDataService();
                    var baseadd = _configuration.GetValue<string>("Api-CMS:BaseAddress");
                    var enpoint = _configuration.GetValue<string>("Api-CMS:Menu");
                    var data = await _masterDataService.PutAsync(baseadd + enpoint, obj);
                    var jsonString = await data.Content.ReadAsStringAsync();
                    var BaseResponse = JsonConvert.DeserializeObject<BaseResponse<List<MenuModel>>>(jsonString);

                    if (BaseResponse.code == Convert.ToInt16(HttpStatusCode.OK))
                    {
                        NotifMessage("success", "Data berhasil dihapus");
                        return RedirectToAction("Index", "Menu");
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
