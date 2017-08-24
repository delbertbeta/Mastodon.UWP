using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastodon.UWP.Model.Settings
{
    public class ApplicationInfo
    {
        public readonly string ClientName = "Mastodon.UWP";

        public readonly string RredirectUris = "https://delbertbeta.cc";

        public readonly string[] Scopes = { "read", "write", "follow" };

        public readonly string Website = "https://delbertbeta.cc";
    }
}
