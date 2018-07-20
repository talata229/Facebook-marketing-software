using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMarketing_Project_CuoiKy.Model
{
    public class Nhom2
    {
        //public string name { get; set; }
        //public string privacy { get; set; }
        //public string version { get; set; }
        //public string id { get; set; }
        [JsonProperty("data")]
        public Nhom3[] data { get; set; }

        [JsonProperty("paging")]
        public object paging { get; set; }
        //public override string ToString()
        //{
        //    return this.paging.ToString;
        //}

    }
}
