using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastodon.API.Apis
{
    public abstract class Base
    {
        public string Domain { get; set; }
        public string AccessToken { get; set; }

        protected Base(string domain, string accessToken)
        {
            Domain = domain;
            AccessToken = accessToken;
        }
    }
}
