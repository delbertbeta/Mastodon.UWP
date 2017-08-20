using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Mastodon.API.Models
{
    public class OauthModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("client_id")]
        public string ClientId { get; set; }
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }
        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; }
    }
}
