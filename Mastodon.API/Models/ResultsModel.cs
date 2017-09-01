using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastodon.API.Models
{
    public class ResultsModel
    {
        [JsonProperty("accounts")]
        public List<AccountModel> Accounts { get; set; }
        [JsonProperty("statuses")]
        public List<StatusModel> Statues { get; set; }
        [JsonProperty("hashtags")]
        public List<string> Hashtags { get; set; }
    }
}
