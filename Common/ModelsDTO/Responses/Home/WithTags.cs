using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.ModelsDTO.Responses.Home
{
    public class WithTags
    {
        [JsonProperty("data")]
        public List<CategoryList> Data { get; set; }
    }
}