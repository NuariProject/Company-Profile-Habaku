namespace api_cms.Object
{
    public class Section
    {
        public class ListSections
        {
            public int section_id { get; set; }
            public int menu_id { get; set; }
            public string section_name { get; set; }
            public int section_number { get; set; }
            public int section_approve { get; set; }
            public bool status { get; set; }
        }

        public class ParamCreateSection
        {
            public int menu_id { get; set; }
            public string section_name { get; set; }
            public int section_number { get; set; }
            public int section_approve { get; set; }
            public bool status { get; set; }
            public string created_by { get; set; }

        }

        public class ParamUpdateSection
        {
            public int section_id { get; set; }
            public int menu_id { get; set; }
            public string section_name { get; set; }
            public int section_number { get; set; }
            public int section_approve { get; set; }
            public bool status { get; set; }
            public string modified_by { get; set; }

        }

    }
}
