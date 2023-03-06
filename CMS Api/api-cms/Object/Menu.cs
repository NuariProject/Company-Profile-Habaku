namespace CMS_API.Object
{
    public class Menu
    {
        public string menu_name { get; set; }
        //public bool status { get; set; }
        //public DateTime created_at { get; set; }
        public string created_by { get; set; }

    }

    public class ListMenus
    {
        public int menu_id { get; set; }
        public string menu_name { get; set; }
        public bool status { get; set; }
    }

    public class UpdateMenu
    {
        public int menu_id { get; set; }
        public string menu_name { get; set; }
        public bool status { get; set; }
        public string modified_by { get; set; }

    }
}
