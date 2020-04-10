using Newtonsoft.Json;

namespace Facebook_Chat.Models.Response.CovidVN
{
    public class CovidRoot
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }
}
