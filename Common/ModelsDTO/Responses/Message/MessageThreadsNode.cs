using System.Net.Mime;
using Newtonsoft.Json;

namespace Common.ModelsDTO.Responses.Message
{
    public class MessageThreadsNode
    {
        [JsonProperty("thread_key")]
        public ThreadKey ThreadKey { get; set; }

        [JsonProperty("all_participants")]
        public AllParticipants AllParticipants { get; set; }

        [JsonProperty("messages_count")]
        public long MessagesCount { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("thread_type")]
        public string ThreadType { get; set; }
    }
}