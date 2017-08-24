using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastodon.API.Models
{
    public class StatusModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("in_reply_to_id")]
        public long? InReplyToId { get; set; }
        [JsonProperty("in_reply_to_account_id")]
        public long InReplyToAccountId { get; set; }
        [JsonProperty("sensitive")]
        public bool Sensitive { get; set; }
        [JsonProperty("spoiler_text")]
        public string SpoilerText { get; set; }
        [JsonProperty("visibility")]
        public string Visibility { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("application")]
        public ApplicationModel Application { get; set; }
        [JsonProperty("account")]
        public AccountModel Account { get; set; }
        [JsonProperty("media_attachments")]
        public List<AttachmentModel> MediaAttachments { get; set; }
        [JsonProperty("mentions")]
        public List<object> Mentions { get; set; }
        [JsonProperty("id")]
        public List<object> tags { get; set; }
        [JsonProperty("id")]
        public string uri { get; set; }
        [JsonProperty("id")]
        public string content { get; set; }
        [JsonProperty("id")]
        public string url { get; set; }
        [JsonProperty("id")]
        public int reblogs_count { get; set; }
        [JsonProperty("id")]
        public int favourites_count { get; set; }
        [JsonProperty("id")]
        public List<object> pixiv_cards { get; set; }
        [JsonProperty("id")]
        public bool pinned { get; set; }
        [JsonProperty("id")]
        public Reblog reblog { get; set; }
        [JsonProperty("id")]
        public object favourited { get; set; }
        [JsonProperty("id")]
        public object reblogged { get; set; }
        [JsonProperty("id")]
        public object muted { get; set; }
    }
}
