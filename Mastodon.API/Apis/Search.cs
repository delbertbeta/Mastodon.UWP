using Mastodon.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastodon.API.Apis
{
    public class Search
    {
        static public async Task<ResultsModel> QuerySearch(string domain, string accessToken, string query)
        {
            return await Services.HttpManager.GetAsync<ResultsModel>($"{domain}{Services.Url.Search}", accessToken, "q=" + query);
        }
    }
}
