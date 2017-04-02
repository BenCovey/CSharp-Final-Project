using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Collections.ObjectModel;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod]
        public async void TestObjectRecievedFromYoutube()
        {
            string query = "Video";
            int maxResults = 10;
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyAEmP6-BFQbwDeVmYkA0fK2fpA_oTTJrv0",
                ApplicationName = this.GetType().ToString()
            });
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = query; // Replace with your search term.
            searchListRequest.MaxResults = maxResults;
            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();
            if(searchListResponse == null)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async void TestCountOfObjectsReturned()
        {
            string query = "Video";
            int maxResults = 10;
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyAEmP6-BFQbwDeVmYkA0fK2fpA_oTTJrv0",
                ApplicationName = this.GetType().ToString()
            });
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = query; // Replace with your search term.
            searchListRequest.MaxResults = maxResults;
            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();
            int count = 1;
            foreach (var searchResult in searchListResponse.Items)
            {
                count++;
            }
            Assert.AreEqual(count, 10);
        }
        [TestMethod]
        public async void TestEmptySearch()
        {
            string query = "";
            int maxResults = 10;
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyAEmP6-BFQbwDeVmYkA0fK2fpA_oTTJrv0",
                ApplicationName = this.GetType().ToString()
            });
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = query; // Replace with your search term.
            searchListRequest.MaxResults = maxResults;
            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();
            int count = 1;
            foreach (var searchResult in searchListResponse.Items)
            {
                count++;
            }
            Assert.AreEqual(count, 10);
        }
        [TestMethod]
        public async void TestWhenMaxResultsAreZero()
        {
            string query = "Video";
            int maxResults = 0;
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyAEmP6-BFQbwDeVmYkA0fK2fpA_oTTJrv0",
                ApplicationName = this.GetType().ToString()
            });
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = query; // Replace with your search term.
            searchListRequest.MaxResults = maxResults;
            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();
            
        }
    }
    class Videos
    {

        public Videos(string VidID, string ChanId, string title, string desc, string link)
        {
            this.VideoID = VidID;
            this.ChannelID = ChanId;
            this.Title = title;
            this.Description = desc;
            this.Link = link;
        }


        public string VideoID { get; set; }
        public string ChannelID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }
}
