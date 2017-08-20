using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Mastodon.API.Services
{
    class HttpManager
    {
        static public async Task<TModel> PostAsync<TModel>(string url, string token, HttpContent param)
        {
            using (var httpClient = new HttpClient())
            {
                if (!url.StartsWith("http"))
                {
                    url = "https://" + url;
                }
                if (string.IsNullOrEmpty(token))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
                using (var res = await httpClient.PostAsync(url, param))
                {
                    return JsonConvert.DeserializeObject<TModel>(await res.Content.ReadAsStringAsync());
                }
            }
        }

        static public async Task<TModel> GetAsync<TModel>(string url, string token, string param)
        {
            using (var httpClient = new HttpClient())
            {
                if (!url.StartsWith("http"))
                {
                    url = "https://" + url;
                }
                if (string.IsNullOrEmpty(token))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
                using (var res = await httpClient.GetAsync(url+"&"+param))
                {
                    return JsonConvert.DeserializeObject<TModel>(await res.Content.ReadAsStringAsync());
                }
            }
        }
    }
}
