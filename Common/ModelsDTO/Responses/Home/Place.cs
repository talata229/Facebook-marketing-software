using Newtonsoft.Json;

namespace Common.ModelsDTO.Responses.Home
{
    public class Place
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

    }
}