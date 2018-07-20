using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMarketing_Project_CuoiKy.Model.Json
{
    public class Friend
    {
        [JsonProperty("friends")]
        public Friend2 friends { get; set; }

        [JsonProperty("id")]
        public string id { get; set; }
    }
}
