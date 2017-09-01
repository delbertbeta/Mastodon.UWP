using Mastodon.API.Models;
using Mastodon.UWP.Pages;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Mastodon.UWP.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SearchView : Page
    {
        ObservableCollection<StatusModel> Statuses = new ObservableCollection<StatusModel>();
        ObservableCollection<AccountModel> Accounts = new ObservableCollection<AccountModel>();

        string QueryText { get; set; }

        public SearchView()
        {
            this.InitializeComponent();
        }

        private async void QueryTextBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            Statuses.Clear();
            Accounts.Clear();
            var account = App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex];
            var result = await API.Apis.Search.QuerySearch(account.Instance.Uri, account.Token.AccessToken, QueryText);
            if (result.Accounts.Count > 0)
            {
                NotFoundAccountTextBlock.Visibility = Visibility.Collapsed;
                Services.ListPushHelper.PushToList(result.Accounts, ref Accounts, Services.ListPushHelper.PushMethod.Foot);
            }
            else
            {
                NotFoundAccountTextBlock.Visibility = Visibility.Visible;
            }
            if (result.Statues.Count > 0)
            {
                NotFoundStatusTextBlock.Visibility = Visibility.Collapsed;
                Services.ListPushHelper.PushToList(result.Statues, ref Statuses, Services.ListPushHelper.PushMethod.Foot);
            }
            else
            {
                NotFoundStatusTextBlock.Visibility = Visibility.Visible;
            }
            
        }

        private void ImageBrush_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            (sender as ImageBrush).ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Images/missing.png"));
        }

        private void StatusControl_NavigateToStatusDetail(object status)
        {
            ((Window.Current.Content as Frame).Content as TimeLinePage).DetailViewNavigateTo(typeof(StatusDetailView), status);
        }

        private void StatusControl_FaceImageTouched(object account)
        {
            ((Window.Current.Content as Frame).Content as TimeLinePage).DetailViewNavigateTo(typeof(AccountView), account);
        }

        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ((Window.Current.Content as Frame).Content as TimeLinePage).DetailViewNavigateTo(typeof(AccountView), (sender as StackPanel).DataContext as AccountModel);
        }
    }
}
