using Mastodon.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mastodon.API.Apis
{
    public class Account
    {
        public static async Task<AccountModel> VerifyCredentials(string domain, string accessToken)
        {
             return await Services.HttpManager.GetAsync<AccountModel>($"{domain}{Services.Url.VerifyCredentials}", accessToken, null);
        }

        public static async Task<AccountModel> FetchAccount(string domain, string accessToken, string id)
        {
            return await Services.HttpManager.GetAsync<AccountModel>($"{domain}{string.Format(Services.Url.FetchingAccount, id)}", accessToken, null);
        }
    }
}
