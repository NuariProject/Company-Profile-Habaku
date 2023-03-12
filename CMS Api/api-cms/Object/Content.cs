namespace api_cms.Object
{
    public class Content
    {
        public class ListContents
        {
            public int content_id { get; set; }
            public int section_id { get; set; }
            public string header { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public string image { get; set; }
            public string url { get; set; }
        }
    }
}
