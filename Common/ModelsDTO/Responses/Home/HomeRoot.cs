using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Common.ModelsDTO.Responses.Home
{
    public class HomeRoot
    {
        [JsonProperty("data")]
        public List<WelcomeDatum> Data { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }
    }
}
