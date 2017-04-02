using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Youtube_Player_Final {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page {
        private ObservableCollection<Model.Video> videos = new ObservableCollection<Model.Video>();
        YouTubeService youtubeService;

        public MainPage() {
            this.InitializeComponent();
            youtubeService = new YouTubeService(new BaseClientService.Initializer() {
                ApiKey = Constants.API_KEY,
                ApplicationName = this.GetType().ToString()
            });
        }

        private async void SearchAsync(string searchTerm) {
            videos = await GetVideosAsync(searchTerm);
            VideosList.ItemsSource = videos;
        }

        private async Task<ObservableCollection<Model.Video>> GetVideosAsync(string query) {
            ObservableCollection<Model.Video> results = new ObservableCollection<Model.Video>();
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = query;
            searchListRequest.MaxResults = 10;
            var searchListResponse = await searchListRequest.ExecuteAsync();
            foreach (var searchResult in searchListResponse.Items) {
                if (searchResult.Id.Kind == "youtube#video") {
                    results.Add(new Model.Video(searchResult.Id.VideoId,
                        searchResult.Snippet.ChannelTitle,
                        searchResult.Snippet.Title,
                        searchResult.Snippet.Description,
                        "https://youtube.com/embed/" + searchResult.Id.VideoId,
                        searchResult.Snippet.Thumbnails.Default__
                        ));
                }
            }
            return results;
        }//E N D task Run

        private async void SearchTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args) {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput) {
                if (sender.Text.Length > 1) {
                    sender.ItemsSource = await GetSuggestions(sender.Text);
                } else {
                    sender.ItemsSource = new string[] { "No suggestions..." };
                }
            }
        }

        private async Task<string[]> GetSuggestions(string query) {
            List<string> suggestions = new List<string>();
            var searchListRequest = youtubeService.Search.List("snippet");

            searchListRequest.Q = query; // Replace with your search term.
            searchListRequest.MaxResults = maxResults;
            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();
            foreach (var searchResult in searchListResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        vidlist.Add(new Videos(searchResult.Id.VideoId, searchResult.Id.ChannelId, searchResult.Snippet.Title, searchResult.Snippet.Description, "https://youtube.com/embed/" + searchResult.Id.VideoId, searchResult.Snippet.Thumbnails.Default__));
                        break;
                }
            }
            return suggestions.ToArray();
        }//END getSuggestions

        private void SearchboxSearch(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args) {
            suggestBox.ItemsSource = null;
            SearchAsync(sender.Text);
        }

        private void VideoSelected(object sender, SelectionChangedEventArgs e) {
            Frame.Navigate(typeof(PlayerPage), new VideoPageSwitchArgs(videos[VideosList.SelectedIndex], suggestBox.Text));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);
            var lastPage = Frame.BackStack.LastOrDefault();
            if (lastPage != null && lastPage.SourcePageType.Equals(typeof(PlayerPage))) {
                string searchTerm = (string)e.Parameter;
                suggestBox.Text = searchTerm;
                SearchAsync(searchTerm);
            }
        }

        private void AboutClick(object sender, RoutedEventArgs e) {
            Frame.Navigate(typeof(About));
        }

        private void ExitClick(object sender, RoutedEventArgs e) {
            CoreApplication.Exit();
        }
    }
}
