using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Mastodon.API.Models
{
    public class AccountModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("acct")]
        public string Acct { get; set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        [JsonProperty("locked")]
        public bool Locked { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("followers_count")]
        public int FollowersCount { get; set; }
        [JsonProperty("following_count")]
        public int FollowingCount { get; set; }
        [JsonProperty("statuses_count")]
        public int StatusesCount { get; set; }
        //[JsonProperty("oauth_authentications")]
        //public List<object> OauthAuthentications { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        [JsonProperty("avatar_static")]
        public string AvatarStatic { get; set; }
        [JsonProperty("header")]
        public string Header { get; set; }
        [JsonProperty("header_static")]
        public string HeaderStatic { get; set; }
    }

    public class MentionModel
    {
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("acct")]
        public string Acct { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
    }
}
