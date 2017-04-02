using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Youtube_Player_Final
{
    class SearchVid
    {

        public ObservableCollection<Videos> videos = new ObservableCollection<Videos>();
        public static string Search = "";
        public  SearchVid()
        {
            Search = MainPage._main.SearchQuery;
            try
            {
                Run();

            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Debug.WriteLine("Error: " + e.Message);
                }
            }
            catch (FileNotFoundException ex)
            {

                Debug.WriteLine("Error: " + ex);

            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            
        }
        private async Task Run()
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyAEmP6-BFQbwDeVmYkA0fK2fpA_oTTJrv0",
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");

            searchListRequest.Q = "AVE"; // Replace with your search term.
            searchListRequest.MaxResults = 5;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();


            //List<string> channels = new List<string>();
            //List<string> playlists = new List<string>();

            // Add each result to the appropriate list, and then display the lists of
            // matching videos, channels, and playlists.
            Console.WriteLine("Videos:");
            foreach (var searchResult in searchListResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        //Console.WriteLine("Title: " + searchResult.Snippet.Title);
                        //Console.WriteLine("http://youtube.com/watch?v=" +searchResult.Id.VideoId);
                        videos.Add(new Videos(searchResult.Id.VideoId, searchResult.Id.ChannelId, searchResult.Snippet.Title, searchResult.Snippet.Description, "http://youtube.com/watch?v=" + searchResult.Id.VideoId));
                        //videos.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.VideoId));
                        break;

                    //case "youtube#channel":
                    //    channels.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.ChannelId));
                    //    break;

                    //case "youtube#playlist":
                    //    playlists.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.PlaylistId));
                    //    break;
                }
            }

            //Console.WriteLine(String.Format("Videos:\n{0}\nhttp://youtube.com/watch?v={1}", string.Join("\n", videos),string.Join("\n", )));
            //Console.WriteLine(String.Format("Channels:\n{0}\n", string.Join("\n", channels)));
            //Console.WriteLine(String.Format("Playlists:\n{0}\n", string.Join("\n", playlists)));
        }
    }
    
}
