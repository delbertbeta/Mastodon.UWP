using Mastodon.API.Models;
using Mastodon.UWP.Controls;
using Mastodon.UWP.Pages;
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
    public sealed partial class MasterView : Page
    {
        
        public MasterView()
        {
            this.InitializeComponent();
        }

        private void ContentPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ContentPivot.SelectedIndex)
            {
                case 1:
                    if (NotificationFrame.Content == null)
                    {
                        var notificationList = new NotificationListControl();
                        notificationList.NavigatingToAccount += (account) =>
                        {
                            NavigatingToAccount?.Invoke(account);
                        };
                        notificationList.NavigateToStatusDetail += (status) =>
                        {
                            ((Window.Current.Content as Frame).Content as TimeLinePage).DetailViewNavigateTo(typeof(StatusDetailView), status);
                        };
                        NotificationFrame.Content = notificationList;

                    }
                    break;
                default:
                    break;
            }
        }

        public delegate void NavigatingToAccountDelegate(AccountModel accountId);
        public event NavigatingToAccountDelegate NavigatingToAccount;

        private void ContentPivot_Loaded(object sender, RoutedEventArgs e)
        {
            var timeline = new Controls.TinelineUserControl();
            timeline.DataContext = new TimelineTypeViewModel
            {
                TimelineType = TimelineType.Home
            };
            timeline.NavigatingToAccount += (account) =>
            {
                NavigatingToAccount?.Invoke(account);
            };
            timeline.NavigateToStatusDetail += (status) =>
            {
                ((Window.Current.Content as Frame).Content as TimeLinePage).DetailViewNavigateTo(typeof(StatusDetailView), status);
            };
            HomeFrame.Content = timeline;
        }
    }
}
