namespace CMS_Dashboard_v1.Models
{
    public class BaseResponse<TModelData>
    {
        public int code { set; get; }
        public string messages { set; get; } = string.Empty;
        public TModelData data { set; get; }
    }
}
