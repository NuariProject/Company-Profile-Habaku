using Microsoft.AspNetCore.Http;

namespace CMS_API.ConfigurationDB
{
    public class HabakuDB
    {
        public static class ConnectionStrings
        {
            public const string HABAKU_CONNECTION = "Data Source=localhost;Initial Catalog=HabakuDB;Integrated Security=True;TrustServerCertificate=True";

        }
        public static class Query
        {
            public const string INSERT_MENU = "INSERT INTO Menu (menu_name, status, created_at, created_by) VALUES (@menu_name, @status, @created_at, @created_by);";
            public const string SELECT_ALL_MENU = "SELECT menu_id, menu_name, status FROM Menu;";
            public const string SELECT_MENU = "SELECT menu_id, menu_name, status FROM Menu WHERE menu_id = @menu_id;";
            public const string UPDATE_MENU = "UPDATE Menu SET menu_name = @menu_name, status = @status, modified_at = @modified_at, modified_by = @modified_by WHERE menu_id = @menu_id;";
        }
    }
}
