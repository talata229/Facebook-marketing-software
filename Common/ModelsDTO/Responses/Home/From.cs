using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.ModelsDTO.Responses.Home
{
    public class From
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("category", NullValueHandling = NullValueHandling.Ignore)]
        public string Category { get; set; }

        [JsonProperty("category_list", NullValueHandling = NullValueHandling.Ignore)]
        public List<CategoryList> CategoryList { get; set; }
    }
}