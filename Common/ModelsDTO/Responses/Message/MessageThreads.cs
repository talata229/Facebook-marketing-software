using Newtonsoft.Json;

namespace Common.ModelsDTO.Responses.Message
{
    public class MessageThreads
    {
        [JsonProperty("nodes")]
        public MessageThreadsNode[] Nodes { get; set; }
    }
}