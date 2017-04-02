using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youtube_Player_Final {
    public class VideoPageSwitchArgs {
        public Model.Video vid;
        public string searchTerm;

        public VideoPageSwitchArgs(Model.Video inVideo, string inSearchTerm) {
            vid = inVideo;
            searchTerm = inSearchTerm;
        }
    }
}
