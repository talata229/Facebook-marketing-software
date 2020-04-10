using Newtonsoft.Json;

namespace Facebook_Chat.Models.Response.CovidVN
{
    public class TrackerByMoh
    {
        [JsonProperty("negative")]
        public string Negative { get; set; }

        [JsonProperty("positive")]
        public string Positive { get; set; }

        [JsonProperty("positive_negative")]
        public string PositiveNegative { get; set; }

        [JsonProperty("serious")]
        public string Serious { get; set; }

        [JsonProperty("total_test")]
        public string TotalTest { get; set; }

        [JsonProperty("waiting_result")]
        public string WaitingResult { get; set; }
    }
}