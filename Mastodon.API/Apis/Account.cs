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

        public static async Task<RelationshipModel> Follow(string domain, string accessToken, int id)
        {
            return await Services.HttpManager.PostAsync<RelationshipModel>($"{domain}{string.Format(Services.Url.Follow, id.ToString())}", accessToken, null);
        }

        public static async Task<RelationshipModel> UnFollow(string domain, string accessToken, int id)
        {
            return await Services.HttpManager.PostAsync<RelationshipModel>($"{domain}{string.Format(Services.Url.UnFollow, id.ToString())}", accessToken, null);
        }
    }
}
