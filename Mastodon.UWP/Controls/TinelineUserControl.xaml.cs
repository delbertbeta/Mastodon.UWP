using Mastodon.API.Models;
using Mastodon.UWP.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    public sealed partial class TinelineUserControl : UserControl
    {
        private TimelineTypeViewModel TimelineType { get { return this.DataContext as TimelineTypeViewModel; } }

        public ObservableCollection<StatusModel> StatusList;

        private string _nextUrl;

        private string _prevUrl;

        public TinelineUserControl()
        {
            this.InitializeComponent();
            StatusList = new ObservableCollection<StatusModel>();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            List<StatusModel> source = await GetSource();
            Services.ListPushHelper.PushToList(source, ref StatusList, Services.ListPushHelper.PushMethod.Foot);
        }

        private async void FreshButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_prevUrl))
            {
                TimelineScrollViewer.ChangeView(null, 0.0, null);
                var account = App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex];
                var result = await API.Apis.Timeline.GetTimelineByUrl(_prevUrl, account.Token.AccessToken);
                _prevUrl = result.PrevUrl;
                Services.ListPushHelper.PushToList(result.Target, ref StatusList, Services.ListPushHelper.PushMethod.Head);
            }
            else
            {
                Services.ListPushHelper.PushToList((await GetSource()), ref StatusList, Services.ListPushHelper.PushMethod.Head);
            }
        }

        private async Task<List<StatusModel>> GetSource()
        {
            var account = App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex];
            List<StatusModel> source;
            if (TimelineType.TimelineType == View.TimelineType.Home)
            {
                var result = await API.Apis.Timeline.GetHomeTimelines(account.Instance.Uri, account.Token.AccessToken, 20);
                source = result.Target;
                _nextUrl = result.NextUrl;
                _prevUrl = result.PrevUrl;
            }

            else if (TimelineType.TimelineType == View.TimelineType.Id)
            {
                var result = await API.Apis.Timeline.GetTimelineById(account.Instance.Uri, account.Token.AccessToken, int.Parse(TimelineType.TimelineIdentifier), 20);
                source = result.Target;
                _nextUrl = result.NextUrl;
                _prevUrl = result.PrevUrl;
                FreshButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                source = new List<StatusModel>();
            }
            return source;
        }

        public delegate void NavigatingToAccountDelegate(AccountModel accountId);
        public event NavigatingToAccountDelegate NavigatingToAccount;

        private void StatusControl_FaceImageTouched(AccountModel account)
        {
            NavigatingToAccount?.Invoke(account);
        }

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
                var result = await API.Apis.Timeline.GetTimelineByUrl(_nextUrl, account.Token.AccessToken);
                _nextUrl = result.NextUrl;
                Services.ListPushHelper.PushToList(result.Target, ref StatusList, Services.ListPushHelper.PushMethod.Foot);
                _isLoading = false;
            }
        }
    }
}
