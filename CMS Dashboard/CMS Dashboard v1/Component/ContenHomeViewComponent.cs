using CMS_Dashboard_v1.Models.ModelForm;
using CMS_Dashboard_v1.Models;
using CMS_Dashboard_v1.Service;
using Microsoft.AspNetCore.Mvc;

namespace CMS_Dashboard_v1.Component
{
    public class ContentHomeViewComponent : ViewComponent
    {
        readonly IConfiguration _configuration;
        GlobalListApi _globallist = new GlobalListApi();
        public ContentHomeViewComponent(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IViewComponentResult> InvokeAsync(ContentLandingPageModel Model)
        {
            var ListContent = await _globallist.GetListContent();
            var ListSection = await _globallist.GetListSection();


            foreach (var item in ListContent)
            {
                Model.ListContent.Add(new ContentModel
                {
                    section_id = item.section_id,
                    header = item.header,
                    title = item.title,
                    description = item.description,
                    image = item.image,
                    url = item.url
                });
            }
            return View(Model);
        }
    }
}
