using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MetaWeather.Common
{
    public class RestClient : IRestClient
    {
        public string BaseUrl { get; set; }
        public string urlParameter { get; set; }

        public async Task<string> RestclientCall()
        {
            using (var client = new HttpClient())
            {
                var concateUrl = string.Concat(BaseUrl, urlParameter);
                using (var response = client.GetAsync(concateUrl).Result)
                {
                    return await response.Content.ReadAsStringAsync();
                }                
            }            

        }

    }
}
