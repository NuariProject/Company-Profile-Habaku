using CMS_Dashboard_v1.Models;
using CMS_Dashboard_v1.Models.ModelForm;

namespace CMS_Dashboard_v1.Service
{
    public class Dammy
    {
        GlobalListApi _globallist = new GlobalListApi();

        public async Task<IList<MenuModel>> MenuList()
        {
           var list = new List<MenuModel>();
            var listmenu = await _globallist.GetListMenu();

            list = (from a in listmenu.Where(ss => ss.status)
                    select a).ToList();

            return list;
        }

        public List<LoginDammyModel> UserList()
        {
            var list = new List<LoginDammyModel>();

            list.Add(new LoginDammyModel { Id = 1, Username = "admin1@admin.com", Password = "12345" });
            list.Add(new LoginDammyModel { Id = 2, Username = "Admin2@admin.com", Password = "12345" });

            return list;
        }

        public List<DropdownModel> StatusList()
        {
            var list = new List<DropdownModel>();
            list.Add(new DropdownModel { value = 1, text = "Aktif" });
            list.Add(new DropdownModel { value = 2, text = "Tidak Aktif" });

            return list;
        }
    }
}
