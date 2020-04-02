using Newtonsoft.Json;

namespace Common.ModelsDTO.Responses.Message
{
    public class AllParticipants
    {
        [JsonProperty("messaging_actor")]
        public MessagingActor MessagingActor { get; set; }
    }
}