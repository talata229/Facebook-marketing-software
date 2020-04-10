using Newtonsoft.Json;

namespace Facebook_Chat.Models.Response.CovidVN
{
    public class HospitalDatum
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }
    }
}