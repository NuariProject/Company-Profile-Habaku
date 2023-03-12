using System.ComponentModel.DataAnnotations;

namespace CMS_Dashboard_v1.Models.ModelForm
{
    public class MenuModel
    {
        public MenuModel()
        {
            List = new List<MenuViewModel>();
        }
        public int menu_id { get; set; }
        public string menu_name { get; set; } = string.Empty;
        public bool status { get; set; }

        public List<MenuViewModel> List { get; set; }
    }

    public class MenuViewModel
    {
        public int menu_id { get; set; }
        [Required(ErrorMessage = "Nama tidak boleh kosong")]
        public string menu_name { get; set; } = string.Empty;
    }

    public class InsertMenuModel
    {
        public string menu_name { get; set; } = string.Empty;
        public string created_by { get; set; } = string.Empty;
    }

    public class PutMenuModel
    {
        public int menu_id { get; set; }
        public string menu_name { get; set; } = string.Empty;
        public bool status { get; set; }
        public string modified_by { get; set; } = string.Empty;
    }
}
