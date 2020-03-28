using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMarketing.Model.Json
{
    public class NewFeed
    {
        [JsonProperty("data")]
        public NewFeed1[] data { get; set; }

        [JsonProperty("paging")]
        public object paging { get; set; }

    }
}
