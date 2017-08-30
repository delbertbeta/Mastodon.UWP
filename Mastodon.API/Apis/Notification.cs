using Mastodon.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastodon.API.Apis
{
    public class Notification
    {
        static public async Task<ModelWithLink<List<NotificationModel>>> GetNotification(string domain, string accessToken)
        {
            return await Services.HttpManager.GetWithLinkAsync<List<NotificationModel>>($"{domain}{Services.Url.Notifications}", accessToken, null);
        }

        static public async Task<ModelWithLink<List<NotificationModel>>> GetNotificationsByUrl(string url, string accessToken)
        {
            return await Services.HttpManager.GetWithLinkAsync<List<NotificationModel>>(url, accessToken, null);
        }
    }
}
