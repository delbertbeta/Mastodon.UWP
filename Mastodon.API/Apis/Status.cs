using Mastodon.API.Models;
using Mastodon.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mastodon.API.Apis
{
    public class Status
    {
        public static async Task<StatusModel> PostStatus(string domain, string accessToken, string status, int? inReplyToId, List<int> mediaIds, bool sensitive, string spoilerText, string visibility)
        {
            var target = new SendStatusModel()
            {
                Status = status,
                InReplyToId = inReplyToId,
                MediaIds = mediaIds,
                Sensitive = sensitive,
                SpoilerText = spoilerText,
                Visibility = visibility
            };
            return await HttpManager.PostJsonAsync<StatusModel, SendStatusModel>($"{domain}{Url.Status}", accessToken, target);
        }

        public static async Task<StatusModel> ReblogStatus(string domain, string accessToken, int id)
        {
            return await HttpManager.PostAsync<StatusModel>($"{domain}{string.Format(Url.Reblog, id)}", accessToken, null);
        }

        public static async Task<StatusModel> UnReblogStatus(string domain, string accessToken, int id)
        {
            return await HttpManager.PostAsync<StatusModel>($"{domain}{string.Format(Url.UnReblog, id)}", accessToken, null);
        }

        public static async Task<StatusModel> FavouritingStatus(string domain, string accessToken, int id)
        {
            return await HttpManager.PostAsync<StatusModel>($"{domain}{string.Format(Url.Favouriting, id)}", accessToken, null);
        }

        public static async Task<StatusModel> UnFavouritingStatus(string domain, string accessToken, int id)
        {
            return await HttpManager.PostAsync<StatusModel>($"{domain}{string.Format(Url.UnFavouriting, id)}", accessToken, null);
        }
    }
}
