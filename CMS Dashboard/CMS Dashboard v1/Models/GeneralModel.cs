namespace CMS_Dashboard_v1.Models
{
    public class GeneralModel
    {
    }

    public class LoginDammyModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class DropdownModel
    {
        public int value { get; set; }
        public string valuestr { get; set; } = string.Empty;
        public string text { get; set; } = string.Empty;
    }
}
