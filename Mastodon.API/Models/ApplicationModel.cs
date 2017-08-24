using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastodon.API.Models
{
    public class ApplicationModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("website")]
        public object Website { get; set; }
    }
}
