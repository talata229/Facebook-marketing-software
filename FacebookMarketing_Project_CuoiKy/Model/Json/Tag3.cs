using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMarketing_Project_CuoiKy.Model
{
    public class Tag3
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("message")]
        public string message { get; set; }

        [JsonProperty("message_tags")]
        public object[] message_tags { get; set; }



        [JsonProperty("created_time")]
        public string created_time { get; set; }


        [JsonProperty("likes")]
        public string likes { get; set; }

        [JsonProperty("from")]
        public Tag4 from { get; set; }

    }
}
