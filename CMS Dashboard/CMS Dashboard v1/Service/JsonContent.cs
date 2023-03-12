using Newtonsoft.Json;
using System.Text;

namespace CMS_Dashboard_v1.Service
{
    public class JsonContent : StringContent
    {
        public JsonContent(object obj) : base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json") { }

        public JsonContent(string obj) : base(obj, Encoding.UTF8, "application/json") { }
    }
}
