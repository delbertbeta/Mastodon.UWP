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

        private string _boostedUsername;
        public string BoostedUsername
        {
            get
            {
                return _boostedUsername + " boosted";
            }
            set
            {
                _boostedUsername = value;

            }
        }

        public StatusControl()
        {
            this.InitializeComponent();
            this.DataContextChanged += StatusControl_DataContextChanged;
        }

        private void StatusControl_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (DataContext != null)
            {
                if (DataContext is StatusModel)
                {
                    var statusModel = DataContext as StatusModel;

                    if (statusModel.Reblog != null)
                    {
                        Status = new StatusViewModel
                        {
                            Username = statusModel.Reblog.Account.Username,
                            Acct = statusModel.Reblog.Account.Acct,
                            CreateAt = statusModel.Reblog.CreatedAt,
                            FaceImage = statusModel.Reblog.Account.AvatarStatic,
                            Content = statusModel.Reblog.Content,
                            Attachment = statusModel.Reblog.MediaAttachments,
                            Id = statusModel.Reblog.Id,
                            Rebloged = statusModel.Reblogged,
                            Favorited = statusModel.Favourited
                        };
                        BoostedUsername = statusModel.Account.Username;
                        BoostedStackPanel.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        Status = new StatusViewModel
                        {
                            Username = statusModel.Account.Username,
                            Acct = statusModel.Account.Acct,
                            CreateAt = statusModel.CreatedAt,
                            FaceImage = statusModel.Account.AvatarStatic,
                            Content = statusModel.Content,
                            Attachment = statusModel.MediaAttachments,
                            Id = statusModel.Id,
                            Rebloged = statusModel.Reblogged,
                            Favorited = statusModel.Favourited
                        };
                    }
                    if (statusModel.Reblogged == true)
                    {
                        Status.ReblogedForeground = "#2B90D9";
                    }
                    else
                    {
                        Status.ReblogedForeground = "#606984";
                    }
                    if (statusModel.Favourited == true)
                    {
                        Status.FavoritedForeground = "#CA8F04";
                    }
                    else
                    {
                        Status.FavoritedForeground = "#606984";
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
        }

        private async void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var index = ImagesGridView.Items.IndexOf((sender as Image).DataContext);
            var contentDialog = new OriginalImageContentDialog();
            contentDialog.DataContext = Status.Attachment[index].Url;
            await contentDialog.ShowAsync();
            e.Handled = true;
        }

        private async void ReplyIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;
            var contentDialog = new SendStatusContentDialog();
            contentDialog.DataContext = this.Status;
            await contentDialog.ShowAsync();
            e.Handled = true;
        }

        private async void ReblogedIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var statusModel = this.DataContext as StatusModel;
            var uri = App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex].Instance.Uri;
            var token = App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex].Token.AccessToken;
            if (Status.Rebloged == true)
            {
                await API.Apis.Status.UnReblogStatus(uri, token, statusModel.Id);
                Status.ReblogedForeground = "#606984";
                Status.Rebloged = false;
                Bindings.Update();
            }
            else
            {
                await API.Apis.Status.ReblogStatus(uri, token, statusModel.Id);
                Status.ReblogedForeground = "#2B90D9";
                Status.Rebloged = true;
                Bindings.Update();
            }
            e.Handled = true;
        }

        private async void FavoritedIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var statusModel = this.DataContext as StatusModel;
            var uri = App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex].Instance.Uri;
            var token = App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex].Token.AccessToken;
            if (Status.Favorited == true)
            {
                await API.Apis.Status.UnFavouritingStatus(uri, token, statusModel.Id);
                Status.FavoritedForeground = "#606984";
                Status.Favorited = false;
                Bindings.Update();
            }
            else
            {
                await API.Apis.Status.FavouritingStatus(uri, token, statusModel.Id);
                Status.FavoritedForeground = "#CA8F04";
                Status.Favorited = true;
                Bindings.Update();
            }
            e.Handled = true;
        }

        public delegate void FaceImageTouchedDelegate(AccountModel account);
        public event FaceImageTouchedDelegate FaceImageTouched;

        private void RoundedFace_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var statusModel = DataContext as StatusModel;
            if (statusModel.Reblog != null)
                FaceImageTouched?.Invoke(statusModel.Reblog.Account);
            else
                FaceImageTouched?.Invoke(statusModel.Account);
            e.Handled = true;
        }

        private void BoostName_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FaceImageTouched?.Invoke((DataContext as StatusModel).Reblog.Account);
            e.Handled = true;
        }

        public delegate void NavigateToStatusDetailDelegate(StatusModel status);
        public event NavigateToStatusDetailDelegate NavigateToStatusDetail;

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            NavigateToStatusDetail?.Invoke(this.DataContext as StatusModel);
            e.Handled = true;
        }

        private void ImageBrush_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            (sender as ImageBrush).ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Images/missing.png"));
        }
    }
}
