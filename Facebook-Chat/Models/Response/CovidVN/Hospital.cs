using System.Collections.Generic;
using Newtonsoft.Json;

namespace Facebook_Chat.Models.Response.CovidVN
{
    public class Hospital
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("data")]
        public List<HospitalDatum> Data { get; set; }
    }
}