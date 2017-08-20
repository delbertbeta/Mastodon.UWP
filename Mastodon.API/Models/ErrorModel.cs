using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastodon.API.Models
{
    public class ErrorModel
    {
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
