using Newtonsoft.Json;

namespace Common.ModelsDTO.Responses.Home
{
    public class Property
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}