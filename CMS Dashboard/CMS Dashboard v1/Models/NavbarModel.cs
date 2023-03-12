namespace CMS_Dashboard_v1.Models
{
    public class NavbarModel
    {
        public NavbarModel()
        {
            load = new List<Content>();
        }
        public string nama { get; set; } = string.Empty;
        public string role { get; set; } = string.Empty;

        public List<Content> load { get; set; }
    }


    public class Content
    {
        public string name { get; set; } = string.Empty;
    }
}
