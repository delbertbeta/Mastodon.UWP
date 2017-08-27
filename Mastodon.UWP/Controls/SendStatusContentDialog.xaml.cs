using Mastodon.API.Models;
using Mastodon.UWP.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“内容对话框”项模板

namespace Mastodon.UWP.Controls
{
    public sealed partial class SendStatusContentDialog : ContentDialog
    {
        public StatusViewModel ContextStatus { get { return this.DataContext as StatusViewModel; } }
        public string Status { get; set; }
        private List<API.Models.AttachmentModel> Attachments = new List<API.Models.AttachmentModel>();
        public ObservableCollection<LocalImagesViewModel> LocalImages = new ObservableCollection<LocalImagesViewModel>();
        public List<StorageFile> LocalImageFiles = new List<StorageFile>();

        public SendStatusContentDialog()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) =>
            {
                if (this.DataContext is StatusViewModel)
                {
                    ReplyStatusGrid.Visibility = Visibility.Visible;
                }
            };
        }

        private void FontIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Hide();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            var files = await picker.PickMultipleFilesAsync();
            if (files.Count > 0)
            {
                // Application now has read/write access to the picked file(s)
                for (int i = 0; i < files.Count && i < (4 - LocalImageFiles.Count); i++)
                {
                    var file = files[i];
                    try
                    {
                        using (var stream = await file.OpenReadAsync())
                        {
                            var bitmap = new BitmapImage();
                            bitmap.SetSource(stream);
                            LocalImages.Add(new LocalImagesViewModel()
                            {
                                Image = bitmap
                            });
                            LocalImageFiles.Add(file);
                        }
                    }
                    catch
                    {
                        // skip that image.
                    }
                }
            }
            else
            {
                // User closed or cancled the picker, do nothing here.
            }
        }

        private void DeleteFontIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var image = (((sender as FontIcon).Parent as Grid).Children[0] as Image).Source as BitmapImage;
            int index = 0;
            foreach (var item in LocalImages)
            {
                if (item.Image == image)
                {
                    index = LocalImages.IndexOf(item);
                    break;
                }
            }
            LocalImages.RemoveAt(index);
            LocalImageFiles.RemoveAt(index);
        }

        private void TootTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int length = 500 - TootTextBox.Text.Length;
            RemainWords.Text = length.ToString();
            if (length < 0 && length == 500)
            {
                TootButton.IsEnabled = false;
            }
            else
            {
                TootButton.IsEnabled = true;
            }
        }

        private async void TootButton_Click(object sender, RoutedEventArgs e)
        {
            var mediaIds = new List<int>();
            TootButton.IsEnabled = false;
            Progress.Visibility = Visibility.Visible;
            int count = 0;
            Progress.Value = 0;
            Progress.Foreground = new SolidColorBrush(Windows.UI.Colors.DeepSkyBlue);
            Progress.Background = new SolidColorBrush(Windows.UI.Colors.DarkGray);
            try
            {
                foreach (var item in LocalImageFiles)
                {
                    var media = await API.Apis.Media.UploadMedia(App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex].Instance.Uri, App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex].Token.AccessToken, item);
                    mediaIds.Add(media.Id);
                    count++;
                    double progress = count / LocalImageFiles.Count;
                    Progress.Value = progress;
                }
                await API.Apis.Status.PostStatus(App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex].Instance.Uri, App.AppSetting.Accounts[App.AppSetting.SelectedAccountIndex].Token.AccessToken, Status, ContextStatus?.Id, mediaIds, false, "", "public");
                this.Hide();
            }
            catch
            {
                TootButton.IsEnabled = true;
                Progress.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                Progress.Background = new SolidColorBrush(Windows.UI.Colors.DarkRed);
            }
        }
    }
}
