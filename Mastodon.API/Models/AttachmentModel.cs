using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastodon.API.Models
{
    public class MetaInfo
    {
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("size")]
        public string Size { get; set; }
        [JsonProperty("aspect")]
        public double Aspect { get; set; }
    }

    public class Meta
    {
        [JsonProperty("original")]
        public MetaInfo Original { get; set; }
        [JsonProperty("small")]
        public MetaInfo Small { get; set; }
    }

    public class AttachmentModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("remote_url")]
        public string RemoteUrl { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("preview_url")]
        public string PreviewUrl { get; set; }
        [JsonProperty("text_url")]
        public string TextUrl { get; set; }
        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }
}
