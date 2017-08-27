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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“内容对话框”项模板

namespace Mastodon.UWP.Controls
{
    public sealed partial class OriginalImageContentDialog : ContentDialog
    {
        public string imageUrl
        {
            get
            {
                return this.DataContext as string;
            }
        }

        public OriginalImageContentDialog()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) =>
            {
                //if (DataContext != null)
                //{
                //    var url = DataContext as string;
                //    if (!string.IsNullOrEmpty(url))
                //    {
                //        image = new ImageViewModel()
                //        {
                //            url = new BitmapImage(new Uri(DataContext as string))
                //        };
                //        Bindings.Update();
                //    }
                //}

                Bindings.Update();
            };
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
