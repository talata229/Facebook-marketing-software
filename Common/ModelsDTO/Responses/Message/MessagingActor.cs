using Newtonsoft.Json;

namespace Common.ModelsDTO.Responses.Message
{
    public class MessagingActor
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}