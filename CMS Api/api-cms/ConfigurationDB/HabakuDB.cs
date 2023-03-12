using Microsoft.AspNetCore.Http;

namespace CMS_API.ConfigurationDB
{
    public class HabakuDB
    {
        public static class ConnectionStrings
        {
            //public const string HABAKU_CONNECTION = "Data Source=FR-000-448\\SQLEXPRESS;Initial Catalog=HabakuDB;Integrated Security=True;TrustServerCertificate=True";/*DB Farhan*/
            public const string HABAKU_CONNECTION = "Data Source=localhost;Initial Catalog=HabakuDB;Integrated Security=True;TrustServerCertificate=True";/*DB Bagas*/

        }
        public static class Query
        {
            #region Menu
            public const string INSERT_MENU = "INSERT INTO Menu (menu_name, status, created_at, created_by) VALUES (@menu_name, @status, @created_at, @created_by);";
            public const string SELECT_ALL_MENU = "SELECT menu_id, menu_name, status FROM Menu;";
            public const string SELECT_MENU = "SELECT menu_id, menu_name, status FROM Menu WHERE menu_id = @menu_id;";
            public const string UPDATE_MENU = "UPDATE Menu SET menu_name = @menu_name, status = @status, modified_at = @modified_at, modified_by = @modified_by WHERE menu_id = @menu_id;";
            #endregion

            #region User
            public const string INSERT_USER = "INSERT INTO [User] (user_name, password, role, created_at, created_by) VALUES (@user_name, @password, @role, @created_at, @created_by);";
            public const string SELECT_ALL_USER = "SELECT user_id, user_name, password, role FROM [User];";
            public const string SELECT_USER = "SELECT user_id, user_name, password, role FROM [User] WHERE user_id = @user_id;";
            public const string UPDATE_USER = "UPDATE [User] SET user_name = @user_name, password = @password, Role = @role, modified_at = @modified_at, modified_by = @modified_by WHERE user_id = @user_id;";

            #endregion

            #region Section
            public const string INSERT_SECTION = "INSERT INTO [Section] (menu_id, section_name, section_number, section_approve, created_at, created_by) VALUES (@menu_id, @section_name, @section_number, @section_approve, @created_at, @created_by);";
            public const string SELECT_ALL_SECTION = "SELECT section_id, menu_id, section_name, section_number, section_approve FROM [Section];";
            public const string SELECT_SECTION = "SELECT section_id, menu_id, section_name, section_number, section_approve FROM [Section] WHERE section_id = @section_id;";
            public const string UPDATE_SECTION = "UPDATE [Section] SET menu_id = @menu_id, section_name = @section_name, section_number = @section_number, section_approve = @section_approve, modified_at = @modified_at, modified_by = @modified_by WHERE section_id = @section_id;";

            #endregion

            #region Content
            public const string INSERT_CONTENT = "INSERT INTO [Content] (section_id, header, title, description, image, url, created_at, created_by) VALUES (@section_id, @header, @title, @description, @image, @url, @created_at, @created_by);";
            public const string SELECT_ALL_CONTENT = "SELECT content_id, section_id, header, title, description, image, url FROM [Content];";

            #endregion
        }
    }
}
