using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMarketing.Model
{
    public class Tag2
    {
        [JsonProperty("count")]
        public string count { get; set; }

        [JsonProperty("data")]
        public Tag3[] data { get; set; }
    }
}
