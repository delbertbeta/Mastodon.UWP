using Mastodon.API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
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
                if (!string.IsNullOrEmpty(token))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
                using (var res = await httpClient.PostAsync(url, param))
                {
                    if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        try
                        {
                            return JsonConvert.DeserializeObject<TModel>(await res.Content.ReadAsStringAsync());
                        }
                        catch (Exception e) when (e is JsonReaderException)
                        {
                            throw new Exception("Not a JSON format document.", e);
                        }
                    }
                    else
                    {
                        try
                        {
                            throw new MastodonException("Illegal API request.", JsonConvert.DeserializeObject<ErrorModel>(await res.Content.ReadAsStringAsync()));
                        }
                        catch (Exception e)
                        {
                            throw new Exception("Not a JSON format document.", e);
                        }
                    }
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
                if (!string.IsNullOrWhiteSpace(token))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
                using (var res = await httpClient.GetAsync(url + (string.IsNullOrWhiteSpace(param) ? "" : "?" + param)))
                {
                    if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        try
                        {
                            var result = await res.Content.ReadAsStringAsync();   
                            return JsonConvert.DeserializeObject<TModel>(result);
                        }
                        catch (Exception e) when (e is JsonReaderException)
                        {
                            throw new Exception("Not a JSON format document.", e);
                        }
                    }
                    else
                    {
                        try
                        {
                            throw new MastodonException("Illegal API request.", JsonConvert.DeserializeObject<ErrorModel>(await res.Content.ReadAsStringAsync()));
                        }
                        catch (Exception e)
                        {
                            throw new Exception("Network Error", e);
                        }
                    }
                }
            }
        }

        static public async Task<ModelWithLink<TModel>> GetWithLinkAsync<TModel>(string url, string token, string param)
        {
            using (var httpClient = new HttpClient())
            {
                if (!url.StartsWith("http"))
                {
                    url = "https://" + url;
                }
                if (!string.IsNullOrWhiteSpace(token))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
                using (var res = await httpClient.GetAsync(url + (string.IsNullOrWhiteSpace(param) ? "" : "?" + param)))
                {
                    if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        try
                        {
                            var model = new ModelWithLink<TModel>();
                            if(res.Headers.TryGetValues("link", out IEnumerable<string> values))
                            {
                                var links = values.FirstOrDefault().Split(',').Select(s => Regex.Match(s, ".*<(.*)>; rel=\"(.*)\"").Groups).ToList();
                                foreach(var link in links)
                                {
                                    if (link[2].Value == "prev")
                                        model.PrevUrl = link[1].Value;
                                    if (link[2].Value == "next")
                                        model.NextUrl = link[1].Value;
                                }
                            }
                            var result = await res.Content.ReadAsStringAsync();
                            model.Target = JsonConvert.DeserializeObject<TModel>(result);
                            return model;
                        }
                        catch (Exception e) when (e is JsonReaderException)
                        {
                            throw new Exception("Not a JSON format document.", e);
                        }
                    }
                    else
                    {
                        try
                        {
                            throw new MastodonException("Illegal API request.", JsonConvert.DeserializeObject<ErrorModel>(await res.Content.ReadAsStringAsync()));
                        }
                        catch (Exception e)
                        {
                            throw new Exception("Network Error", e);
                        }
                    }
                }
            }
        }

        static public async Task<TModel> PostJsonAsync<TModel, ObjectModel>(string url, string token, ObjectModel target)
        {
            using (var httpClient = new HttpClient())
            {
                if (!url.StartsWith("http"))
                {
                    url = "https://" + url;
                }
                if (!string.IsNullOrWhiteSpace(token))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
                var payload = JsonConvert.SerializeObject(target, new JsonSerializerSettings {
                    NullValueHandling = NullValueHandling.Include
                });
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = new StringContent(payload, Encoding.UTF8, "application/json");
                using (var res = await httpClient.SendAsync(request))
                {
                    if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        try
                        {
                            var result = await res.Content.ReadAsStringAsync();
                            return JsonConvert.DeserializeObject<TModel>(result);
                        }
                        catch (Exception e) when (e is JsonReaderException)
                        {
                            throw new Exception("Not a JSON format document.", e);
                        }
                    }
                    else
                    {
                        try
                        {
                            throw new MastodonException("Illegal API request.", JsonConvert.DeserializeObject<ErrorModel>(await res.Content.ReadAsStringAsync()));
                        }
                        catch (Exception e)
                        {
                            throw new Exception("Network Error", e);
                        }
                    }
                }
            }

        }
    }
}
