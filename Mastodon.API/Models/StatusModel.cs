using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastodon.API.Models
{
    public class TagModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }

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
        public List<MentionModel> Mentions { get; set; }
        [JsonProperty("tags")]
        public List<TagModel> Tags { get; set; }
        [JsonProperty("uri")]
        public string Uri { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("reblogs_count")]
        public int ReblogsCount { get; set; }
        [JsonProperty("favourites_count")]
        public int FavouritesCount { get; set; }
        [JsonProperty("pinned")]
        public bool Pinned { get; set; }
        [JsonProperty("reblog")]
        public StatusModel Reblog { get; set; }
        [JsonProperty("favourited")]
        public bool Favourited { get; set; }
        [JsonProperty("reblogged")]
        public bool Reblogged { get; set; }
        [JsonProperty("muted")]
        public bool Muted { get; set; }
    }
}
