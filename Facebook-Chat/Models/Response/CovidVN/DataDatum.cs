using System.Collections.Generic;
using Newtonsoft.Json;

namespace Facebook_Chat.Models.Response.CovidVN
{
    public class DataDatum
    {
        [JsonProperty("hospital")]
        public List<Hospital> Hospital { get; set; }

        [JsonProperty("topics")]
        public List<string> Topics { get; set; }

        [JsonProperty("tracker_by_day")]
        public List<Tracker> TrackerByDay { get; set; }

        [JsonProperty("tracker_by_growth")]
        public List<Tracker> TrackerByGrowth { get; set; }

        [JsonProperty("tracker_by_moh")]
        public TrackerByMoh TrackerByMoh { get; set; }

        [JsonProperty("tracker_by_province")]
        public List<Tracker> TrackerByProvince { get; set; }

        [JsonProperty("tracker_total_by_day")]
        public Tracker TrackerTotalByDay { get; set; }
    }
}