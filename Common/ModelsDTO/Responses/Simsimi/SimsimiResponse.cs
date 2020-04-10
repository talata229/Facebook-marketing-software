using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ModelsDTO.Requests.Simsimi;
using Newtonsoft.Json;

namespace Common.ModelsDTO.Responses.Simsimi
{
    public class SimsimiResponse
    {
        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("statusMessage")]
        public string StatusMessage { get; set; }

        [JsonProperty("request")]
        public SimsimiRequest Request { get; set; }

        [JsonProperty("atext")]
        public string Atext { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }
    }
}
