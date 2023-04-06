using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditCLient.API
{
    public class Token
    {

        public class Rootobject
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public string device_id { get; set; }
            public int expires_in { get; set; }
            public string scope { get; set; }
            public string refresh_token { get; set; }
        }

    }
}
