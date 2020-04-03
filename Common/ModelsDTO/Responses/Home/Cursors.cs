using Newtonsoft.Json;

namespace Common.ModelsDTO.Responses.Home
{
    public class Cursors
    {
        [JsonProperty("after")]
        public string After { get; set; }

        [JsonProperty("before")]
        public string Before { get; set; }
    }
}