
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System.Collections.ObjectModel;
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Youtube_Player_Final
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        
        private ObservableCollection<Videos> videos = new ObservableCollection<Videos>();   
        public static MainPage _main;
        private bool search;
        private int maxResults = 25;
        private ListViewItem itm;

        public MainPage()
        {
            this.InitializeComponent();
            _main = this;
            
        }

        private async void  btnSearch_Click(object sender, RoutedEventArgs e)
        {
            search = true;
            try
            {
                listVideos.Items.Clear();
                videos.Clear();
                videos = await new MainPage().Run(txtSearch.Text);
                int count = videos.Count;
                Debug.WriteLine(count);
                foreach(var video in videos)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Content = video.Title;
                    listVideos.Items.Add(lvi);
                    Debug.WriteLine(video.Link);
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
            
        }

        private async void mediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            //mediaElement.Play();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private async Task<ObservableCollection<Videos>> Run(string query)
        {
            ObservableCollection<Videos> vidlist = new ObservableCollection<Videos>();
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyAEmP6-BFQbwDeVmYkA0fK2fpA_oTTJrv0",
                ApplicationName = this.GetType().ToString()
            });
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = query; // Replace with your search term.
            //
            //
            // THIS IS THE SETTING FOR MAX RESULTS
            //
            //
            searchListRequest.MaxResults = maxResults;
            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();
            // Add each result to the appropriate list, and then display the lists of
            // matching videos, channels, and playlists.
            foreach (var searchResult in searchListResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        vidlist.Add(new Videos(searchResult.Id.VideoId, searchResult.Id.ChannelId, searchResult.Snippet.Title, searchResult.Snippet.Description, "https://youtube.com/embed/" + searchResult.Id.VideoId, searchResult.Snippet.Thumbnails.Default__));
                        //Thumbnail img = searchResult.Snippet.Thumbnails.Default__;
                        Debug.WriteLine(searchResult.Snippet.Title);
                        //listVideos.Text = "\n" + searchResult.Snippet.Title + "\nhttp://youtube.com/watch?v=" + searchResult.Id.VideoId;
                        //listVideos.Items.Add(new ListViewItem { Content = searchResult.Snippet.Title });
                        break;
                }
            }
            return vidlist;
        }

        private void listVideos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var _selected = listVideos.SelectedIndex;
                player.Source = new Uri(videos[_selected].Link);
                Title.Text = "Now Playing: " + videos[_selected].Title;
                Description.Text = videos[_selected].Description;
                Debug.WriteLine(videos[_selected].Title);
            }catch(Exception)
            {
                
            }
        }

        private void AboutClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));
        }
    }



    class Videos
    {
        
        public Videos(string VidID, string ChanId, string title, string desc, string link, Thumbnail thumb)
        {
            this.VideoID = VidID;
            this.ChannelID = ChanId;
            this.Title = title;
            this.Description = desc;
            this.Link = link;
            this.Thumbnail = thumb;
        }


        public string VideoID { get; set; }
        public string ChannelID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public Thumbnail Thumbnail { get; set; }
    }
}
