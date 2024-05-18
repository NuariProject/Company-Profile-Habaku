﻿using Microsoft.AspNetCore.Http;

namespace CMS_API.ConfigurationDB
{
    public class HabakuDB
    {
        public static class ConnectionStrings
        {
            public const string HABAKU_CONNECTION = "Data Source=XXXXXXXX;Initial Catalog=HabakuDB;Integrated Security=True;TrustServerCertificate=True";/*DB Bagas*/

        }
        public static class Query
        {
            #region Menu
            public const string INSERT_MENU = "INSERT INTO Menu (menu_name, status, created_at, created_by) VALUES (@menu_name, @status, @created_at, @created_by);";
            public const string SELECT_ALL_MENU = "SELECT menu_id, menu_name, status FROM Menu ORDER BY menu_id ASC;";
            public const string SELECT_MENU = "SELECT menu_id, menu_name, status FROM Menu WHERE menu_id = @menu_id;";
            public const string UPDATE_MENU = "UPDATE Menu SET menu_name = @menu_name, status = @status, modified_at = @modified_at, modified_by = @modified_by WHERE menu_id = @menu_id;";
            #endregion

            #region User
            public const string INSERT_USER = "INSERT INTO [User] (user_name, password, role, status, created_at, created_by) VALUES (@user_name, @password, @role, @status, @created_at, @created_by);";
            public const string SELECT_ALL_USER = "SELECT user_id, user_name, password, role, status FROM [User];";
            public const string SELECT_USER = "SELECT user_id, user_name, password, role, status FROM [User] WHERE user_id = @user_id;";
            public const string UPDATE_USER = "UPDATE [User] SET user_name = @user_name, password = @password, Role = @role, status = @status, modified_at = @modified_at, modified_by = @modified_by WHERE user_id = @user_id;";

            #endregion

            #region Section
            public const string INSERT_SECTION = "INSERT INTO [Section] (menu_id, section_name, section_number, section_approve, status, created_at, created_by) VALUES (@menu_id, @section_name, @section_number, @section_approve, @status, @created_at, @created_by);";
            public const string SELECT_ALL_SECTION = "SELECT section_id, menu_id, section_name, section_number, section_approve, status FROM [Section] ORDER BY section_id ASC;";
            public const string SELECT_SECTION = "SELECT section_id, menu_id, section_name, section_number, section_approve, status FROM [Section] WHERE section_id = @section_id;";
            public const string UPDATE_SECTION = "UPDATE [Section] SET menu_id = @menu_id, section_name = @section_name, section_number = @section_number, section_approve = @section_approve, status = @status, modified_at = @modified_at, modified_by = @modified_by WHERE section_id = @section_id;";

            #endregion

            #region Content
            public const string INSERT_CONTENT = "INSERT INTO [Content] (section_id, header, title, description, image, url, status, created_at, created_by) VALUES (@section_id, @header, @title, @description, @image, @url, @status, @created_at, @created_by);";
            public const string SELECT_ALL_CONTENT = "SELECT content_id, section_id, header, title, description, image, url, status, created_at FROM [Content] ORDER BY content_id ASC;";
            public const string SELECT_CONTENT = "SELECT content_id, section_id, header, title, description, image, url, status, created_at FROM [Content] WHERE content_id = @content_id;";
            public const string UPDATE_CONTENT = "UPDATE [Content] SET section_id = @section_id, header = @header, title = @title, description = @description, image = @image, url = @url, status = @status, modified_at = @modified_at, modified_by = @modified_by WHERE content_id = @content_id;";

            #endregion
        }
    }
}
