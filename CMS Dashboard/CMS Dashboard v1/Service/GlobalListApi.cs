using CMS_Dashboard_v1.Models.ModelForm;
using CMS_Dashboard_v1.Models;
using Newtonsoft.Json;
using CMS_Dashboard_v1.Areas.Form.Controllers;

namespace CMS_Dashboard_v1.Service
{
    public class GlobalListApi
    {
        readonly IConfiguration _configuration;
        public GlobalListApi()
        {
            var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
        }

        public async Task<List<MenuModel>> GetListMenu()
        {
            MasterDataService _masterDataService = new MasterDataService();
            var baseadd = _configuration.GetValue<string>("Api-CMS:BaseAddress");
            var enpoint = _configuration.GetValue<string>("Api-CMS:Menu");
            var data = new BaseResponse<List<MenuModel>>();
            var list = new List<MenuModel>();
            var response = await _masterDataService.GetAsync(baseadd + enpoint);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<BaseResponse<List<MenuModel>>>(jsonString);
                list = data.data;
            }

            return list;
        }

        public async Task<List<SectionModel>> GetListSection()
        {
            MasterDataService _masterDataService = new MasterDataService();
            var baseadd = _configuration.GetValue<string>("Api-CMS:BaseAddress");
            var enpoint = _configuration.GetValue<string>("Api-CMS:Section");
            var data = new BaseResponse<List<SectionModel>>();
            var list = new List<SectionModel>();
            var response = await _masterDataService.GetAsync(baseadd + enpoint);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<BaseResponse<List<SectionModel>>>(jsonString);
                list = data.data;
            }

            return list;
        }

        public async Task<List<ContentModel>> GetListContent()
        {
            MasterDataService _masterDataService = new MasterDataService();
            var baseadd = _configuration.GetValue<string>("Api-CMS:BaseAddress");
            var enpoint = _configuration.GetValue<string>("Api-CMS:Content");
            var data = new BaseResponse<List<ContentModel>>();
            var list = new List<ContentModel>();
            var response = await _masterDataService.GetAsync(baseadd + enpoint);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<BaseResponse<List<ContentModel>>>(jsonString);
                list = data.data;
            }

            return list;
        }
    }
}
