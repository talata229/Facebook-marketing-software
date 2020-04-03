using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.ModelsDTO.Responses.Home
{
    public class CommentsDatum
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("from")]
        public From From { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("created_time")]
        public string CreatedTime { get; set; }

        [JsonProperty("likes", NullValueHandling = NullValueHandling.Ignore)]
        public long? Likes { get; set; }

        [JsonProperty("message_tags", NullValueHandling = NullValueHandling.Ignore)]
        public List<PurpleMessageTag> MessageTags { get; set; }
    }
}