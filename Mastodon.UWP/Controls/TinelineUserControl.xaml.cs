using Mastodon.API.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        public ObservableCollection<StatusModel> StatusList;

        public TinelineUserControl()
        {
            this.InitializeComponent();
            StatusList = new ObservableCollection<StatusModel>();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var account = App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex];
            var source = await API.Apis.Timeline.GetHomeTimelines(account.Instance.Uri, account.Token.AccessToken, null, null, 20);
            Services.ListPushHelper.PushToList(source, ref StatusList, Services.ListPushHelper.PushMethod.Foot);
        }

        private async void FreshButton_Click(object sender, RoutedEventArgs e)
        {
            var account = App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex];
            var source = await API.Apis.Timeline.GetHomeTimelines(account.Instance.Uri, account.Token.AccessToken, null, null, 20);
            StatusList.Clear();
            Services.ListPushHelper.PushToList(source, ref StatusList, Services.ListPushHelper.PushMethod.Foot);
        }
    }
}
