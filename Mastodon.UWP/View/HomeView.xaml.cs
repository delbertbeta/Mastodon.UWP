using Mastodon.API.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Mastodon.UWP.View
{
    public sealed partial class HomeView : UserControl
    {
        public ObservableCollection<StatusModel> StatusList;

        public HomeView()
        {
            this.InitializeComponent();
            StatusList = new ObservableCollection<StatusModel>();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            StatusList.Add(new StatusModel());
            StatusList.Add(new StatusModel());

            StatusList.Add(new StatusModel());
            StatusList.Add(new StatusModel());
            StatusList.Add(new StatusModel());
            StatusList.Add(new StatusModel());
            StatusList.Add(new StatusModel());
            StatusList.Add(new StatusModel());
            StatusList.Add(new StatusModel());
            StatusList.Add(new StatusModel());


        }
    }
}
