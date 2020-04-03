using Newtonsoft.Json;

namespace Common.ModelsDTO.Responses.Home
{
    public class CategoryList
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}