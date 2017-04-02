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


namespace Youtube_Player_Final {
    public sealed partial class PlayerPage : Page {
        Model.Video currentVideo;
        string lastSearchTerm;

        public PlayerPage() {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);
            VideoPageSwitchArgs args = (VideoPageSwitchArgs)e.Parameter;
            currentVideo = args.vid;
            lastSearchTerm = args.searchTerm;

            player.Source = new Uri(currentVideo.Link);
            descriptionBlock.Text = currentVideo.Description;
        }

        private void GoBack(object sender, RoutedEventArgs e) {
            player.Navigate(new Uri("about:blank"));
            Frame.Navigate(typeof(MainPage), lastSearchTerm);
        }
    }
}
