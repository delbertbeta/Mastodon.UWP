using Mastodon.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Mastodon.API.Apis
{
    public class Oauth
    {
        public static async Task<TokenModel> GetTokenByPassword(string domain, string clientId, string clientSecret, string grantType, string username, string password)
        {
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(new StringContent(clientId), "client_id");
                formData.Add(new StringContent(clientSecret), "client_secret");
                formData.Add(new StringContent(grantType), "grant_type");
                formData.Add(new StringContent(username), "username");
                formData.Add(new StringContent(password), "password");
                return await Services.HttpManager.PostAsync<TokenModel>($"{domain}{Services.Url.UserLoginViaApi}", null, formData);
            }
        }
    }
}
