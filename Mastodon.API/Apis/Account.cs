using Mastodon.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mastodon.API.Apis
{
    class Account
    {
        public static async Task<AccountModel> GetAccount(string domain, string accessToken, string id)
        {
            if (id == null)
            {
                return await Services.HttpManager.GetAsync<AccountModel>($"{domain}{Services.Url.VerifyCredentials}", accessToken, null);
            }
            else
            {
                return await Services.HttpManager.GetAsync<AccountModel>(string.Format(Services.Url.FetchingAccount, id), accessToken, null);
            }
        }
    }
}
