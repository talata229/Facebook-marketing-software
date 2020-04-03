using Newtonsoft.Json;

namespace Common.ModelsDTO.Responses.Home
{
    public class Privacy
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("friends")]
        public string Friends { get; set; }

        [JsonProperty("allow")]
        public string Allow { get; set; }

        [JsonProperty("deny")]
        public string Deny { get; set; }
    }
}