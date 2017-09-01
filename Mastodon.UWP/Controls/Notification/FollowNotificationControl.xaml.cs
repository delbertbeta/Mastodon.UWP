using Mastodon.API.Models;
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

namespace Mastodon.UWP.Controls.Notification
{
    public sealed partial class FollowNotificationControl : UserControl
    {
        NotificationModel Notification { get { return this.DataContext as NotificationModel; } }

        public FollowNotificationControl()
        {
            this.InitializeComponent();
            DataContextChanged += (s, e) =>
            {
                if (DataContext != null)
                {
                    Bindings.Update();
                }
            };
        }

        public delegate void FaceImageTouchedDelegate(AccountModel account);
        public event FaceImageTouchedDelegate FaceImageTouched;

        private void BaseStackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.FaceImageTouched?.Invoke(Notification.Account);
        }

        private void ImageBrush_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            (sender as ImageBrush).ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Images/missing.png"));
        }
    }
}
