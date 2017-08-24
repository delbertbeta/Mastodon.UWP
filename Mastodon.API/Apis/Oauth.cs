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
        public static async Task<TokenModel> GetTokenByPassword(string domain, string clientId, string clientSecret, string username, string password)
        {
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(new StringContent(clientId), "client_id");
                formData.Add(new StringContent(clientSecret), "client_secret");
                formData.Add(new StringContent("password"), "grant_type");
                formData.Add(new StringContent(username), "username");
                formData.Add(new StringContent(password), "password");
                return await Services.HttpManager.PostAsync<TokenModel>($"{domain}{Services.Url.UserLoginViaApi}", null, formData);
            }
        }

        public static async Task<TokenModel> GetTokenByCode(string domain, string clientId, string clientSecret, string code, string redirectUri)
        {
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(new StringContent(clientId), "client_id");
                formData.Add(new StringContent(clientSecret), "client_secret");
                formData.Add(new StringContent("authorization_code"), "grant_type");
                formData.Add(new StringContent(code), "code");
                formData.Add(new StringContent(redirectUri), "redirect_uri");
                return await Services.HttpManager.PostAsync<TokenModel>($"{domain}{Services.Url.UserLoginViaApi}", null, formData);
            }
        }
    }
}
