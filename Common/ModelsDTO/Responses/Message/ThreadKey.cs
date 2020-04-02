using Newtonsoft.Json;

namespace Common.ModelsDTO.Responses.Message
{
    public class ThreadKey
    {
        [JsonProperty("thread_fbid")]
        public string ThreadFbid { get; set; }

        [JsonProperty("other_user_id")]
        public string OtherUserId { get; set; }
    }
}