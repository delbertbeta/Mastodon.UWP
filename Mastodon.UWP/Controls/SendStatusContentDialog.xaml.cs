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
        public string Status { get; set; }
        private List<API.Models.AttachmentModel> Attachments = new List<API.Models.AttachmentModel>();
        public ObservableCollection<LocalImagesViewModel> LocalImages = new ObservableCollection<LocalImagesViewModel>();
        public List<StorageFile> LocalImageFiles = new List<StorageFile>();

        public SendStatusContentDialog()
        {
            this.InitializeComponent();
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
                for (int i = 0; i < files.Count && i < 4; i++)
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
    }
}
