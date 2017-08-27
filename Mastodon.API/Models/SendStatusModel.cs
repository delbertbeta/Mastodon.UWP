using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastodon.API.Models
{
    public class SendStatusModel
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("in_reply_to_id")]
        public int? InReplyToId { get; set; }
        [JsonProperty("media_ids")]
        public List<int> MediaIds { get; set; }
        [JsonProperty("sensitive")]
        public bool Sensitive { get; set; }
        [JsonProperty("spoiler_text")]
        public string SpoilerText { get; set; }
        [JsonProperty("visibility")]
        public string Visibility { get; set; }
    }
}
