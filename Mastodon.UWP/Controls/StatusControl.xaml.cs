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
                    Attachment = statusModel.MediaAttachments
                };
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
    }
}
