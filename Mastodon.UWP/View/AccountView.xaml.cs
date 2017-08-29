using Mastodon.API.Models;
using Mastodon.UWP.Controls;
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
    public sealed partial class AccountView : Page
    {
        public AccountViewModel Account { get; set; }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var accountModel = e.Parameter as AccountModel;
            this.Account = new AccountViewModel
            {
                Username = accountModel.Username,
                Acct = accountModel.Acct,
                FaceImage = accountModel.AvatarStatic,
                Followers = accountModel.FollowersCount,
                Follows = accountModel.FollowingCount,
                HeaderImage = accountModel.HeaderStatic,
                Id = accountModel.Id,
                Note = accountModel.Note,
                Posts = accountModel.StatusesCount,
            };
            var relationship = (await API.Apis.Relationships.GetRelationships(App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex].Instance.Uri, App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex].Token.AccessToken, accountModel.Id)).FirstOrDefault();
            Account.Relationship = relationship;
            if (accountModel.Id == App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex].AccountModel.Id)
            {
                ChangeToUnfollow();
                FollowButton.IsEnabled = false;
            }
            else if (relationship.Following == true)
            {
                ChangeToUnfollow();
            }
            else
            {
                ChangeToFollow();
            }
            var timeline = new TinelineUserControl();
            timeline.DataContext = new TimelineTypeViewModel
            {
                TimelineType = TimelineType.Id,
                TimelineIdentifier = Account.Id.ToString()
            };
            ContainerFrame.Content = timeline;
        }

        private void ChangeToUnfollow()
        {
            FollowButton.Content = "Unfollow";
            FollowButton.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 49, 53, 67));
        }

        private void ChangeToFollow()
        {
            FollowButton.Content = "Follow";
            FollowButton.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 37, 137, 208));
        }

        public AccountView()
        {
            this.InitializeComponent();
        }

        private async void FollowButton_Click(object sender, RoutedEventArgs e)
        {
            if (Account.Relationship.Following == true)
            {
                Account.Relationship = await API.Apis.Account.UnFollow(App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex].Instance.Uri, App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex].Token.AccessToken, Account.Id);
                ChangeToFollow();
            } else
            {
                Account.Relationship = await API.Apis.Account.Follow(App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex].Instance.Uri, App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex].Token.AccessToken, Account.Id);
                ChangeToUnfollow();
            }
        }

        private async void AccountSrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (AccountSrollViewer.VerticalOffset >= AccountSrollViewer.ScrollableHeight - 50)
            {
                await (ContainerFrame.Content as TinelineUserControl).UpdateTimeline();
            }
        }
    }
}
