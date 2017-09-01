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
    public sealed partial class StatusNotificationControl : UserControl
    {
        private NotificationModel Notification
        {
            get
            {
                return this.DataContext as NotificationModel;
            }
        }

        public StatusNotificationControl()
        {
            this.InitializeComponent();
            this.DataContextChanged += StatusNotificationControl_DataContextChanged;
        }

        private void StatusNotificationControl_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (DataContext != null)
            {
                if (Notification.Type == "mention")
                    ChangeToMetionState();
                if (Notification.Type == "reblog")
                    ChangeToReblogState();
                if (Notification.Type == "favourite")
                    ChangeToFavoritedState();
            }

        }

        private void ChangeToMetionState()
        {
            TypeExplain.Text = " metioned you";
            Icon.Icon = FontAwesome.UWP.FontAwesomeIcon.At;
            Icon.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 43, 145, 217));
        }

        private void ChangeToReblogState()
        {
            TypeExplain.Text = " boosted your status";
            Icon.Icon = FontAwesome.UWP.FontAwesomeIcon.Retweet;
            Icon.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 43, 145, 217));
        }


        private void ChangeToFavoritedState()
        {
            TypeExplain.Text = " favourited your status";
            Icon.Icon = FontAwesome.UWP.FontAwesomeIcon.Star;
            Icon.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 202, 143, 4));
        }

        public delegate void FaceImageTouchedDelegate(AccountModel account);
        public event FaceImageTouchedDelegate FaceImageTouched;

        private void StatusControl_FaceImageTouched(AccountModel account)
        {
            this.FaceImageTouched?.Invoke(account);
        }

        public delegate void NavigateToStatusDetailDelegate(StatusModel status);
        public event NavigateToStatusDetailDelegate NavigateToStatusDetail;

        private void StatusControl_NavigateToStatusDetail(StatusModel status)
        {
            this.NavigateToStatusDetail?.Invoke(status);
        }
    }
}
