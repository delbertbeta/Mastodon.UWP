using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastodon.API.Services
{
    internal sealed class Url
    {
        private const string prefix = "/api/v1";

        public const string AppsRegister = prefix + "/apps";

        public const string UserLoginViaApi = "/oauth/token";

        public const string VerifyCredentials = prefix + "/accounts/verify_credentials";

        public const string FetchingAccount = prefix + "/accounts/{0}";

        public const string Instance = prefix + "/instance";
    }
}
