using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMarketing_Project_CuoiKy.Model
{
    public class Nhom
    {
        [JsonProperty("groups")]
        public Nhom2 groups { get; set; }

        [JsonProperty("id")]
        public string id { get; set; }


    }
}
