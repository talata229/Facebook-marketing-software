using Newtonsoft.Json;

namespace Common.ModelsDTO.Responses.Home
{
    public class Likes
    {
        [JsonProperty("count")]
        public long Count { get; set; }
    }
}