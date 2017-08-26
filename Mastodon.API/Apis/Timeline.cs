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
        static public async Task<List<StatusModel>> GetHomeTimelines(string domain, string accessToken, long? maxId, long? sinceId, int limit = 20)
        {
            string param = "";
            param += (maxId == null) ? "" : ("max_id=" + maxId.ToString()) + "&";
            param += (sinceId == null) ? "" : ("since_id=" + sinceId.ToString()) + "&";
            param += "limit=" + limit.ToString();
            return await Services.HttpManager.GetAsync<List<StatusModel>>($"{domain}{Services.Url.TimelineHome}", accessToken, null);
        }
    }
}
