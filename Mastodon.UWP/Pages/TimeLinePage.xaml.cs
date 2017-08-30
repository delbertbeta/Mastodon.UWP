using Mastodon.API.Models;
using Mastodon.UWP.Services;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Mastodon.UWP.Pages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class TimeLinePage : Page
    {
        public SimpleProfile profit;
        public TimeLinePage()
        {
            this.InitializeComponent();
            profit = new SimpleProfile()
            {
                Username = App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex].AccountModel.DisplayName,
                Instance = "at " + App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex].Instance.Title,
                FaceImage = App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex].AccountModel.AvatarStatic,
                HeaderImage = App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex].AccountModel.HeaderStatic
                //FaceImage = new BitmapImage(),
                //HeaderImage = new BitmapImage()
            };
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MasterFrame.Navigate(typeof(View.MasterView));
            (MasterFrame.Content as View.MasterView).NavigatingToAccount += TimeLinePage_NavigatingToAccount;
            DetailFrame.Navigate(typeof(View.StandByView));
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += BackRequested;
            DetailFrame.Navigated += DetailFrame_Navigated;
        }

        private void DetailFrame_Navigated(object sender, NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = DetailFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : Windows.UI.Core.AppViewBackButtonVisibility.Collapsed;
        }

        public void DetailViewNavigateTo(Type frame, object param)
        {
            DetailFrame.Navigate(frame, param);
        }

        private void BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (DetailFrame == null)
                return;
            if (DetailFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                DetailFrame.GoBack();
            }
        }

        private void TimeLinePage_NavigatingToAccount(AccountModel account)
        {
            DetailFrame.Navigate(typeof(View.AccountView), account);
        }

        private void FontIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MenuSplitView.IsPaneOpen = !MenuSplitView.IsPaneOpen;
        }

        private void Page_Loading(FrameworkElement sender, object args)
        {
            //var faceImageFile = await WebResourceManager.GetWebResource(App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex].AccountModel.AvatarStatic, ResourceType.Face);
            //using (var faceImageStream = await faceImageFile.OpenAsync(Windows.Storage.FileAccessMode.Read))
            //    await profit.FaceImage.SetSourceAsync(faceImageStream);
            //var headerImageFile = await WebResourceManager.GetWebResource(App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex].AccountModel.HeaderStatic, ResourceType.Header);
            //using (var headerImageStream = await headerImageFile.OpenAsync(Windows.Storage.FileAccessMode.Read))
            //    await profit.HeaderImage.SetSourceAsync(headerImageStream);
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            var contentDialog = new Controls.SendStatusContentDialog();
            await contentDialog.ShowAsync();
            
        }
    }
}

