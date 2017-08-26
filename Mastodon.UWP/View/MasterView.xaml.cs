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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Mastodon.UWP.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MasterView : Page
    {
        
        public MasterView()
        {
            this.InitializeComponent();
        }

        private void ContentPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ContentPivot.SelectedIndex)
            {
                case 0:
                    
                default:
                    break;
            }
        }

        private void ContentPivot_Loaded(object sender, RoutedEventArgs e)
        {
            HomeFrame.Content = new View.HomeView();
        }
    }
}
