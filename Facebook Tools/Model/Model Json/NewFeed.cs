using Facebook_Tools.Model.Model_Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook_Tools.Model
{
    public class NewFeed
    {
        [JsonProperty("data")]
        public NewFeed1[] data { get; set; }

        [JsonProperty("paging")]
        public NewFeed2 paging { get; set; }

    }
}
