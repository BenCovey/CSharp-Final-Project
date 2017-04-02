using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youtube_Player_Final.Model {
    public class Video {

        public Video(string VidID, string channelTitle, string title, string desc, string link, Thumbnail thumb) {
            this.VideoID = VidID;
            this.ChannelTitle = channelTitle;
            this.Title = title;
            this.Description = desc;
            this.Link = link;
            this.Thumbnail = thumb;
        }

        public string VideoID { get; set; }
        public string ChannelTitle { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public Thumbnail Thumbnail { get; set; }
    }
}
