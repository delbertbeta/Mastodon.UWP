using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastodon.API.Models
{
    public class RelationshipModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("following")]
        public bool Following { get; set; }
        [JsonProperty("followed_by")]
        public bool FollowedBy { get; set; }
        [JsonProperty("blocking")]
        public bool Blocking { get; set; }
        [JsonProperty("muting")]
        public bool Muting { get; set; }
        [JsonProperty("requested")]
        public bool Requested { get; set; }
        [JsonProperty("domain_blocking")]
        public bool DomainBlocking { get; set; }
    }
}
