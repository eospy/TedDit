using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditCLient.MVVM.Model
{
    public class VideoData
    {

        public class Rootobject
        {
            public Reddit_Video reddit_video { get; set; }
        }

        public class Reddit_Video
        {
            public int bitrate_kbps { get; set; }
            public string fallback_url { get; set; }
            public int height { get; set; }
            public int width { get; set; }
            public string scrubber_media_url { get; set; }
            public string dash_url { get; set; }
            public int duration { get; set; }
            public string hls_url { get; set; }
            public bool is_gif { get; set; }
            public string transcoding_status { get; set; }
        }

    }
}
