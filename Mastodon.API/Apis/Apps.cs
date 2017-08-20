using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mastodon.API.Models;
using Mastodon.API.Services;
using System.Net.Http;

namespace Mastodon.API.Apis
{
    public class Apps
    {
        static public async Task<OauthModel> Register(string domain, string clientName, string redicrectUris, string[] scopes, string website)
        {
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(new StringContent(clientName), "client_name");
                formData.Add(new StringContent(redicrectUris), "redirect_uris");
                formData.Add(new StringContent(string.Join(" ", scopes)), "scopes");
                formData.Add(new StringContent(website), "website");
                return await HttpManager.PostAsync<OauthModel>($"{domain}{Url.AppsRegister}", null, formData);
            }
        }
    }
}
