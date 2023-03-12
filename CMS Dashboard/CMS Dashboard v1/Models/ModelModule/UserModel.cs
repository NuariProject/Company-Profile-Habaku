namespace CMS_Dashboard_v1.Models.ModelModule
{
    public class UserModel
    {
        public int user_id { get; set; }
        public string user_name { get; set; } = string.Empty;
        public string pasasword { get; set; } = string.Empty;
        public string role { get; set; } = string.Empty;
    }
}
