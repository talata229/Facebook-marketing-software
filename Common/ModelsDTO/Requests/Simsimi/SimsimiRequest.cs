using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Common.ModelsDTO.Requests.Simsimi
{
    public class SimsimiRequest
    {
        [JsonProperty("utext")]
        public string Utext { get; set; }
        [JsonProperty("lang")]
        public string Lang { get; set; }
    }
}
