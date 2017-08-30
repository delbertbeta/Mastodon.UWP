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
            if (Notification.Type == "mention")
                VisualStateManager.GoToState(this, "Metioned", false);
            if (Notification.Type == "reblog")
                VisualStateManager.GoToState(this, "Rebloged", false);
            if (Notification.Type == "favourite")
                VisualStateManager.GoToState(this, "Favourited", false);
        }

        private void StatusControl_FaceImageTouched(AccountModel account)
        {
            
        }

        private void StatusControl_NavigateToStatusDetail(StatusModel status)
        {

        }
    }
}
