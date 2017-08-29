using Mastodon.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastodon.API.Apis
{
    public class Relationships
    {
        static public async Task<List<RelationshipModel>> GetRelationships(string domain, string accessToken, int id)
        {
            return await Services.HttpManager.GetAsync<List<RelationshipModel>>($"{domain}{Services.Url.Relationships}", accessToken, $"id={id.ToString()}");
        }
    }
}
