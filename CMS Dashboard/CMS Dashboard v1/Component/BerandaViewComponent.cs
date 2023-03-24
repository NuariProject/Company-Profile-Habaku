using CMS_Dashboard_v1.Models.ModelForm;
using CMS_Dashboard_v1.Models;
using CMS_Dashboard_v1.Service;
using Microsoft.AspNetCore.Mvc;

namespace CMS_Dashboard_v1.Component
{
    public class BerandaViewComponent : ViewComponent
    {
        readonly IConfiguration _configuration;
        GlobalListApi _globallist = new GlobalListApi();
        public BerandaViewComponent(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IViewComponentResult> InvokeAsync(ContentLandingPageModel Model)
        {
            var ListContent = await _globallist.GetListContent();
            var ListSection = await _globallist.GetListSection();
            var ListMenu = await _globallist.GetListMenu();

            Model.ListContent = (from a in ListContent
                                 join b in ListSection on a.section_id equals b.section_id
                                 join c in ListMenu on b.menu_id equals c.menu_id
                                 where a.status && b.status && c.status && c.menu_id == 1
                                 select new ContentModel
                                 {
                                     section_id = b.section_id,
                                     content_id = a.content_id,
                                     section = b.section_number,
                                     header = a.header,
                                     title = a.title,
                                     image = a.image,
                                     url = a.url,
                                     description = a.description
                                 }).ToList();

            return View(Model);
        }
    }
}
