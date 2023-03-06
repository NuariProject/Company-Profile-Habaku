namespace CMS_API.Object
{
    public class GeneralResponse
    {
        public string Code { get; set; }
        public string Messages { get; set; }
    }
    public class GeneralResponseData<T>
    {
        public string Code { get; set; }
        public string Messages { get; set; }
        public T Data { get; set; }
    }
}
