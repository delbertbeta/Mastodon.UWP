using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Mastodon.API.Models
{
    public class ContextModel
    {
        [JsonProperty("ancestors")]
        public List<StatusModel> Ancestors { get; set; }
        [JsonProperty("descendants")]
        public List<StatusModel> Descendants { get; set; }
    }
}
