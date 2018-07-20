using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMarketing_Project_CuoiKy.Model
{
    public class Tag1
    {
        [JsonProperty("comments")]
        public Tag2 comments { get; set; }

        [JsonProperty("id")]
        public string id { get; set; }

    }
}
