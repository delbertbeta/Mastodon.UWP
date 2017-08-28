using Mastodon.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastodon.API.Apis
{
    public class Timeline
    {
        static public async Task<List<StatusModel>> GetHomeTimelines(string domain, string accessToken, int limit = 20)
        {
            string param = "";
            param += "limit=" + limit.ToString();
            return await Services.HttpManager.GetAsync<List<StatusModel>>($"{domain}{Services.Url.TimelineHome}", accessToken, null);
        }

        static public async Task<List<StatusModel>> GetTimelineByUrl(string url, string accessToken)
        {
            return await Services.HttpManager.GetAsync<List<StatusModel>>(url, accessToken, null);
        }

        static public async Task<List<StatusModel>> GetTimelineById(string domain, string accessToken, int id, int limit = 20)
        {
            string param = "limit=" + limit.ToString();
            return await Services.HttpManager.GetAsync<List<StatusModel>>($"{domain}{string.Format(Services.Url.GetTimelineById, id)}", accessToken, param);
        }
    }
}
