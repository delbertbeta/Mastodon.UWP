using Mastodon.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastodon.API.Apis
{
    public class Instance
    {
        static public async Task<InstanceModel> GetInstance(string domain)
        {
            return await Services.HttpManager.GetAsync<InstanceModel>($"{domain}{Services.Url.Instance}", null, null);
        }
    }
}
