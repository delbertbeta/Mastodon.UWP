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
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Mastodon.UWP.Controls.Notification
{
    public sealed partial class BaseControl : UserControl
    {
        public BaseControl()
        {
            this.InitializeComponent();
            DataContextChanged += BaseControl_DataContextChanged;
        }

        private void BaseControl_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (DataContext != null)
            {
                var notification = DataContext as NotificationModel;
                if (notification.Type == "follow")
                {
                    var content = new FollowNotificationControl();
                    content.DataContext = notification;
                    content.FaceImageTouched += Control_FaceImageTouched;
                    ContainerFrame.Content = content;
                }
                else
                {
                    var content = new StatusNotificationControl();
                    content.DataContext = notification;
                    content.NavigateToStatusDetail += Control_NavigateToStatusDetail;
                    content.FaceImageTouched += Control_FaceImageTouched;
                    ContainerFrame.Content = content;
                }
            }
        }

        public delegate void FaceImageTouchedDelegate(AccountModel account);
        public event FaceImageTouchedDelegate FaceImageTouched;


        public delegate void NavigateToStatusDetailDelegate(StatusModel status);
        public event NavigateToStatusDetailDelegate NavigateToStatusDetail;

        private void Control_FaceImageTouched(AccountModel account)
        {
            this.FaceImageTouched?.Invoke(account);
        }

        private void Control_NavigateToStatusDetail(StatusModel status)
        {
            this.NavigateToStatusDetail?.Invoke(status);
        }
    }
}
