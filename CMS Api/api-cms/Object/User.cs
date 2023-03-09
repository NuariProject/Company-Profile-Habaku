namespace api_cms.Object
{
    public class User
    {
        public class ListUsers
        {
            public int user_id { get; set; }
            public string user_name { get; set; }
            public string role { get; set; }
        }

        public class ParamCreateUser
        {
            public string user_name { get; set; }
            public string password { get; set; }
            public string role { get; set; }
            public string created_by { get; set; }
        }

        public class ParamUpdateUser
        {
            public int user_id { get; set; }
            public string user_name { get; set; }
            public string password { get; set; }
            public string role { get; set; }
            public string modified_by { get; set; }
        }
    }
}
