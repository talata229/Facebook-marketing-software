using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMarketing_Project_CuoiKy.Model.Json
{
    public class Friend2
    {
        [JsonProperty("data")]
        public Friend3[] data { get; set; }

        [JsonProperty("paging")]
        public object paging { get; set; }
    }
}
