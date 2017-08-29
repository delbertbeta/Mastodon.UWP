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

        public const string TimelineHome = prefix + "/timelines/home";

        public const string Media = prefix + "/media";

        public const string Status = prefix + "/statuses";

        public const string Reblog = prefix + "/statuses/{0}/reblog";

        public const string UnReblog = prefix + "/statuses/{0}/unreblog";

        public const string Favouriting = prefix + "/statuses/{0}/favourite";

        public const string UnFavouriting = prefix + "/statuses/{0}/unfavourite";

        public const string GetTimelineById = prefix + "/accounts/{0}/statuses";

        public const string Relationships = prefix + "/accounts/relationships";

        public const string Follow = prefix + "/accounts/{0}/follow";

        public const string UnFollow = prefix + "/accounts/{0}/unfollow";
    }
}
