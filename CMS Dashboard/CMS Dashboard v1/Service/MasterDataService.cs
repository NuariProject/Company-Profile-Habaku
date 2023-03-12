using CMS_Dashboard_v1.Models;
using System.Configuration;

namespace CMS_Dashboard_v1.Service
{
    public class MasterDataService
    {
        public async Task<HttpResponseMessage> GetAsync(string uri)
        {
            var result = new HttpResponseMessage();

            try
            {
                using (var client = new HttpClient())
                {
                    result = await client.GetAsync(uri);

                    result.EnsureSuccessStatusCode();
                }
                   
            }
            catch (Exception ex)
            {
                result.ReasonPhrase = ex.ToString();
            }

            return result;
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string uri, T model) where T : class
        {
            var result = new HttpResponseMessage();

            try
            {
                using (var client = new HttpClient())
                {
                    result = await client.PostAsync(uri, new JsonContent(model));

                    result.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                result.ReasonPhrase = ex.ToString();
            }

            return result;
        }
        public async Task<HttpResponseMessage> PutAsync<T>(string uri, T model) where T : class
        {
            var result = new HttpResponseMessage();

            try
            {
                using (var client = new HttpClient())
                {
                    result = await client.PutAsync(uri, model != null ? new JsonContent(model) : null);

                    result.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                result.ReasonPhrase = ex.ToString();
            }

            return result;
        }
    }
}
