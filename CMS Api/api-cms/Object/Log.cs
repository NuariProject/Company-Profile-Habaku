namespace CMS_API.Object
{
    public class Log
    {
        public class WriteFileLogResult
        {
            public string id { get; set; }
            public string trxDate { get; set; }
            public string trxTime { get; set; }
            public object result { get; set; }
            public object param { get; set; }
            public object paramExt { get; set; }

        }
    }
}
