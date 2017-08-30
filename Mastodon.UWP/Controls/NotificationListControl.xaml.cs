using Mastodon.API.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Mastodon.UWP.Controls
{
    public sealed partial class NotificationListControl : UserControl
    {
        public NotificationListControl()
        {
            this.InitializeComponent();
        }

        public ObservableCollection<NotificationModel> Notifications;

        private string _nextUrl;

        private string _prevUrl;

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            List<NotificationModel> source = await GetSource();
            Services.ListPushHelper.PushToList(source, ref Notifications, Services.ListPushHelper.PushMethod.Foot);
        }

        private async void FreshButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_prevUrl))
            {
                TimelineScrollViewer.ChangeView(null, 0.0, null);
                var account = App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex];
                var result = await API.Apis.Notification.GetNotificationsByUrl(_prevUrl, account.Token.AccessToken);
                _prevUrl = result.PrevUrl;
                Services.ListPushHelper.PushToList(result.Target, ref Notifications, Services.ListPushHelper.PushMethod.Head);
            }
            else
            {
                Services.ListPushHelper.PushToList((await GetSource()), ref Notifications, Services.ListPushHelper.PushMethod.Head);
            }
        }

        private async Task<List<NotificationModel>> GetSource()
        {
            var account = App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex];
            List<NotificationModel> source;
            var result = await API.Apis.Notification.GetNotification(account.Instance.Uri, account.Token.AccessToken);
            source = result.Target;
            _nextUrl = result.NextUrl;
            _prevUrl = result.PrevUrl;
            return source;
        }

        public delegate void NavigatingToAccountDelegate(AccountModel accountId);
        public event NavigatingToAccountDelegate NavigatingToAccount;

        private void StatusControl_FaceImageTouched(AccountModel account)
        {
            NavigatingToAccount?.Invoke(account);
        }

        public delegate void NavigateToStatusDetailDelegate(StatusModel status);
        public event NavigateToStatusDetailDelegate NavigateToStatusDetail;

        private bool _isLoading = false;

        private async void TimelineScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (TimelineScrollViewer.VerticalOffset >= TimelineScrollViewer.ScrollableHeight - 50)
            {
                await UpdateTimeline();
            }
        }

        public async Task UpdateTimeline()
        {
            if (_isLoading || string.IsNullOrEmpty(_nextUrl))
                return;
            else
            {
                var account = App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex];
                _isLoading = true;
                var result = await API.Apis.Notification.GetNotificationsByUrl(_nextUrl, account.Token.AccessToken);
                _nextUrl = result.NextUrl;
                Services.ListPushHelper.PushToList(result.Target, ref Notifications, Services.ListPushHelper.PushMethod.Foot);
                _isLoading = false;
            }
        }

        private void StatusControl_NavigateToStatusDetail(StatusModel status)
        {
            NavigateToStatusDetail?.Invoke(status);
        }

    }
}
