using Mastodon.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastodon.API.Apis
{
    public class Favorites
    {
        static public async Task<ModelWithLink<List<StatusModel>>> GetFavorites(string domian, string accessToken)
        {
            return await Services.HttpManager.GetWithLinkAsync<List<StatusModel>>($"{domian}{Services.Url.Favorites}", accessToken, null);
        }

        static public async Task<ModelWithLink<List<StatusModel>>> GetFavoritesByUrl(string url, string accessToken)
        {
            return await Services.HttpManager.GetWithLinkAsync<List<StatusModel>>(url, accessToken, null);
        }
    }
}
