using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.ModelsDTO.Responses.Home
{
    public class Comments
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public List<CommentsDatum> Data { get; set; }
    }
}