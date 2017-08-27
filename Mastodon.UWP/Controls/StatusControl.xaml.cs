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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Mastodon.UWP.Controls
{
    public sealed partial class StatusControl : UserControl
    {
        public StatusViewModel Status { get; set; }

        public string ReblogedForeground { get; set; } = "#606984";

        public string FavoritedForeground { get; set; } = "#606984";

        public StatusControl()
        {
            this.InitializeComponent();
            this.DataContextChanged += StatusControl_DataContextChanged;
        }

        private void StatusControl_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (DataContext != null)
            {
                var statusModel = DataContext as StatusModel;
                Status = new StatusViewModel
                {
                    Username = statusModel.Account.Username,
                    Acct = statusModel.Account.Acct,
                    CreateAt = statusModel.CreatedAt,
                    FaceImage = statusModel.Account.AvatarStatic,
                    Content = statusModel.Content,
                    Attachment = statusModel.MediaAttachments,
                    Id = statusModel.Id
                };
                if (statusModel.Reblog != null)
                {
                    Status.ReplyStatus = new StatusViewModel
                    {
                        Username = statusModel.Reblog.Account.Username,
                        Acct = statusModel.Reblog.Account.Acct,
                        CreateAt = statusModel.Reblog.CreatedAt,
                        FaceImage = statusModel.Reblog.Account.AvatarStatic,
                        Content = statusModel.Reblog.Content,
                        Attachment = statusModel.Reblog.MediaAttachments,
                        Id = statusModel.Reblog.Id
                    };
                    ReplyStackPanel.Visibility = Visibility.Visible;
                }
                if (statusModel.Reblogged == true)
                {
                    ReblogedForeground = "#2B90D9";
                }
                else
                {
                    ReblogedForeground = "#606984";
                }
                if (statusModel.Favourited == true)
                {
                    FavoritedForeground = "#CA8F04";
                }
                else
                {
                    FavoritedForeground = "#606984";
                }
                Bindings.Update();
                //var faceFile = await WebResourceManager.GetWebResource(statusModel.Account.AvatarStatic, ResourceType.Face);
                //using (var stream = await faceFile.OpenAsync(Windows.Storage.FileAccessMode.Read, Windows.Storage.StorageOpenOptions.AllowOnlyReaders))
                //{
                //    Status.FaceImage.SetSource(stream);
                //}
                //Bindings.Update();
            }
        }

        private async void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var index = ImagesGridView.Items.IndexOf((sender as Image).DataContext);
            var contentDialog = new OriginalImageContentDialog();
            contentDialog.DataContext = Status.Attachment[index].Url;
            await contentDialog.ShowAsync();
        }

        private async void ReplyIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var contentDialog = new SendStatusContentDialog();
            contentDialog.DataContext = this.Status;
            await contentDialog.ShowAsync();
        }

        private async void ReblogedIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var statusModel = this.DataContext as StatusModel;
            var uri = App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex].Instance.Uri;
            var token = App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex].Token.AccessToken;
            if (statusModel.Reblogged == true)
            {
                await API.Apis.Status.UnReblogStatus(uri, token, statusModel.Id);
                FavoritedForeground = "#606984";
                Bindings.Update();
            }
            else
            {
                this.DataContext = await API.Apis.Status.ReblogStatus(uri, token, statusModel.Id);
                ReblogedForeground = "#2B90D9";
                Bindings.Update();
            }

            
        }

        private async void FavoritedIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var statusModel = this.DataContext as StatusModel;
            var uri = App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex].Instance.Uri;
            var token = App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex].Token.AccessToken;
            if (statusModel.Favourited == true)
            {
                await API.Apis.Status.UnFavouritingStatus(uri, token, statusModel.Id);
                FavoritedForeground = "#606984";
                Bindings.Update();
            }
            else
            {
                await API.Apis.Status.FavouritingStatus(uri, token, statusModel.Id);
                FavoritedForeground = "#CA8F04";
                Bindings.Update();
            }
        }
    }
}
