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
    public class SectionController : Controller
    {
        readonly IConfiguration _configuration;
        GlobalListApi _globallist = new GlobalListApi();
        public SectionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(SectionModel Model)
        {
            var ListSection = await _globallist.GetListSection();
            var ListMenu = await _globallist.GetListMenu();

            try
            {
                if (ListSection != null && ListMenu != null)
                {
                    Model.List = (from a in ListSection//.Where(ss => ss.status)
                                  join b in ListMenu//.Where(ss => ss.status)
                                  on a.menu_id equals b.menu_id
                                  select new SectionViewModel
                                  {
                                      section_id = a.section_id,
                                      menu_id =  a.menu_id,
                                      menu_name = b.menu_name,
                                      section_name = a.section_name,
                                      section_number = a.section_number,
                                      section_approve = a.section_approve

                                  }).ToList();
                }
                else
                {
                    NotifMessage("error", "Gagal load data Section");
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

        //private async Task<> Dropdown(SectionViewModel Model)
        //{
        //    var general = new GeneralService();
        //    //ViewBag.MenuList = new SelectList(general.Dropdown(), "text", "text");
        //    //ViewBag.MenuList = new SelectList(await general.DropdownMenu(), "value", "text");
        //    var dropdownData = await general.DropdownMenu();
        //    Model.MenuList = new SelectList(dropdownData, "value", "text").ToList();
        //}


        [Route("Section/Create")]
        public async Task<IActionResult> Create()
        {
            var model = new SectionViewModel();
            var general = new GeneralService();
            var dropdownData = await general.DropdownMenu();
            model.MenuList = new SelectList(dropdownData, "menu_id", "menu_name").ToList();
            return View(model);
        }

        [HttpPost]
        [Route("Section/Create")]
        public async Task<IActionResult> Create(SectionViewModel model)
        {
            var response = await _globallist.GetListSection();
            if (response.Any(ss => /*ss.status &&*/ ss.section_name == model.section_name))
                ModelState.AddModelError("section_name", "Nama Section sudah terdaftar");
            if (response.Any(ss => /*ss.status &&*/ ss.section_name == model.section_name 
                                             && ss.section_number == model.section_number))
                ModelState.AddModelError("section_number", "Urutan Section sudah terdaftar");

            try
            {
                if (ModelState.IsValid)
                {
                    var obj = new InsertSectionModel 
                    { 
                        menu_id = Convert.ToInt16(model.menu_id),
                        section_name = model.section_name,
                        section_number = model.section_number,
                        section_approve = model.section_approve,
                        created_by = HttpContext.Session.GetString("Username") 
                    };

                    MasterDataService _masterDataService = new MasterDataService();
                    var baseadd = _configuration.GetValue<string>("Api-CMS:BaseAddress");
                    var enpoint = _configuration.GetValue<string>("Api-CMS:Section");
                    var data = await _masterDataService.PostAsync(baseadd + enpoint, obj);
                    string jsonString = await data.Content.ReadAsStringAsync();
                    var BaseResponse = JsonConvert.DeserializeObject<BaseResponse<List<SectionModel>>>(jsonString);

                    if (BaseResponse.code == Convert.ToInt16(HttpStatusCode.OK))
                    {
                        NotifMessage("success", "Data berhasil disimpan");
                        return RedirectToAction("Index", "Section");
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
            //Dropdown(model);
            return View(model);
        }

        public async Task<SectionViewModel> FindData(int id)
        {
            var model = new SectionViewModel();
            var response = await _globallist.GetListSection();
            var value = response.Where(ss => ss.section_id == id/* && ss.status*/).FirstOrDefault();

            model.menu_id = value.menu_id;
            model.section_name = value.section_name;
            model.section_number = value.section_number;
            model.section_approve = value.section_approve;
            model.section_id = value.section_id;

            return (model);
        }

        [HttpGet]
        [Route("Section/Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await FindData(id);

            return View(model);
        }

        [HttpPost]
        [Route("Section/Edit")]
        public async Task<IActionResult> Edit(SectionViewModel model)
        {
            var response = await _globallist.GetListSection();
            if (response.Any(ss => /*ss.status &&*/ ss.section_name == model.section_name && ss.section_id != model.section_id))
                ModelState.AddModelError("section_name", "Nama Section sudah terdaftar");
            if (response.Any(ss => /*ss.status &&*/ ss.section_name == model.section_name
                                             && ss.section_number == model.section_number
                                             && ss.section_id != model.section_id))
                ModelState.AddModelError("section_number", "Urutan Section sudah terdaftar");

            try
            {
                if (ModelState.IsValid)
                {
                    var obj = new PutSectionModel
                    {
                        section_id = model.section_id,
                        menu_id = Convert.ToInt16(model.menu_id),
                        section_name = model.section_name,
                        section_number = model.section_number,
                        section_approve = model.section_approve,
                        //status = true,
                        modified_by = HttpContext.Session.GetString("Username")
                    };

                    MasterDataService _masterDataService = new MasterDataService();
                    var baseadd = _configuration.GetValue<string>("Api-CMS:BaseAddress");
                    var enpoint = _configuration.GetValue<string>("Api-CMS:Section");
                    var data = await _masterDataService.PutAsync(baseadd + enpoint, obj);
                    string jsonString = await data.Content.ReadAsStringAsync();
                    var BaseResponse = JsonConvert.DeserializeObject<BaseResponse<List<MenuModel>>>(jsonString);

                    if (BaseResponse.code == Convert.ToInt16(HttpStatusCode.OK))
                    {
                        NotifMessage("success", "Data berhasil diubah");
                        return RedirectToAction("Index", "Section");
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
            //Dropdown(model);
            return View(model);
        }

        [Route("Section/Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await FindData(id);

            return View(model);
        }

        [HttpPost]
        [Route("Section/Delete")]
        public async Task<IActionResult> Delete(SectionViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var obj = new PutSectionModel
                    {
                        section_id = model.section_id,
                        menu_id = Convert.ToInt16(model.menu_id),
                        section_name = model.section_name,
                        section_number = model.section_number,
                        section_approve = model.section_approve,
                        //status = false,
                        modified_by = HttpContext.Session.GetString("Username")
                    };

                    MasterDataService _masterDataService = new MasterDataService();
                    var baseadd = _configuration.GetValue<string>("Api-CMS:BaseAddress");
                    var enpoint = _configuration.GetValue<string>("Api-CMS:Section");
                    var data = await _masterDataService.PutAsync(baseadd + enpoint, obj);
                    var jsonString = await data.Content.ReadAsStringAsync();
                    var BaseResponse = JsonConvert.DeserializeObject<BaseResponse<List<MenuModel>>>(jsonString);

                    if (BaseResponse.code == Convert.ToInt16(HttpStatusCode.OK))
                    {
                        NotifMessage("success", "Data berhasil dihapus");
                        return RedirectToAction("Index", "Section");
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
            //Dropdown(model);
            return View(model);
        }

    }
}
