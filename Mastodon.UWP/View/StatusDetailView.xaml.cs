using Mastodon.API.Models;
using Mastodon.UWP.Controls;
using Mastodon.UWP.Pages;
using Mastodon.UWP.ViewModel;
using System;
using System.Collections.Generic;
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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Mastodon.UWP.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class StatusDetailView : Page
    {
        private StatusModel BaseStatus { get; set; }

        public StatusDetailView()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            BaseStatus = e.Parameter as StatusModel;
            var context = await API.Apis.Status.GetContext(App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex].Instance.Uri, App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex].Token.AccessToken, BaseStatus.Id);
            if (context.Ancestors.Count > 0)
            {
                var timeline = new TinelineUserControl();
                timeline.DataContext = new TimelineTypeViewModel
                {
                    TimelineType = TimelineType.DataContext,
                    Source = context.Ancestors
                };
                timeline.NavigateToStatusDetail += Timeline_NavigateToStatusDetail;
                timeline.NavigatingToAccount += Timeline_NavigatingToAccount;
                AncestorsFrame.Content = timeline;
            }
            if (context.Descendants.Count > 0)
            {
                var timeline = new TinelineUserControl();
                timeline.DataContext = new TimelineTypeViewModel
                {
                    TimelineType = TimelineType.DataContext,
                    Source = context.Descendants
                };
                timeline.NavigateToStatusDetail += Timeline_NavigateToStatusDetail;
                timeline.NavigatingToAccount += Timeline_NavigatingToAccount;
                DescendantsFrame.Content = timeline;
            }
        }

        private void Timeline_NavigatingToAccount(AccountModel accountId)
        {
            ((Window.Current.Content as Frame).Content as TimeLinePage).DetailViewNavigateTo(typeof(AccountView), accountId);
        }

        private void Timeline_NavigateToStatusDetail(StatusModel status)
        {
            ((Window.Current.Content as Frame).Content as TimeLinePage).DetailViewNavigateTo(typeof(StatusDetailView), status);
        }
    }
}
