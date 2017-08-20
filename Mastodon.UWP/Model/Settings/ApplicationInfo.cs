using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastodon.UWP.Model.Settings
{
    public class ApplicationInfo
    {
        public const string ClientName = "Mastodon.UWP";

        public const string RredirectUris = "urn:ietf:wg:oauth:2.0:oob";

        public readonly string[] Scopes = { "read", "write", "follow" };

        public const string Website = "https://delbertbeta.cc";
    }
}
