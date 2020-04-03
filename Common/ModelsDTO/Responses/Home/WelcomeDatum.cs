using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.ModelsDTO.Responses.Home
{
    public class WelcomeDatum
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("from")]
        public From From { get; set; }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty("picture", NullValueHandling = NullValueHandling.Ignore)]
        public string Picture { get; set; }

        [JsonProperty("link", NullValueHandling = NullValueHandling.Ignore)]
        public string Link { get; set; }

        [JsonProperty("icon", NullValueHandling = NullValueHandling.Ignore)]
        public string Icon { get; set; }

        [JsonProperty("actions")]
        public List<ActionDTO> Actions { get; set; }

        [JsonProperty("privacy")]
        public Privacy Privacy { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("status_type", NullValueHandling = NullValueHandling.Ignore)]
        public string StatusType { get; set; }

        [JsonProperty("object_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ObjectId { get; set; }

        [JsonProperty("created_time")]
        public DateTime? CreatedTime { get; set; }

        [JsonProperty("updated_time")]
        public string UpdatedTime { get; set; }

        [JsonProperty("is_hidden")]
        public bool IsHidden { get; set; }

        [JsonProperty("is_expired")]
        public bool IsExpired { get; set; }

        [JsonProperty("likes", NullValueHandling = NullValueHandling.Ignore)]
        public Likes Likes { get; set; }

        [JsonProperty("comments")]
        public Comments Comments { get; set; }

        [JsonProperty("story", NullValueHandling = NullValueHandling.Ignore)]
        public string Story { get; set; }

        [JsonProperty("story_tags", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, List<StoryTag>> StoryTags { get; set; }

        [JsonProperty("shares", NullValueHandling = NullValueHandling.Ignore)]
        public Likes Shares { get; set; }

        [JsonProperty("message_tags", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, List<FluffyMessageTag>> MessageTags { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("with_tags", NullValueHandling = NullValueHandling.Ignore)]
        public WithTags WithTags { get; set; }

        [JsonProperty("caption", NullValueHandling = NullValueHandling.Ignore)]
        public string Caption { get; set; }

        [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
        public string Source { get; set; }

        [JsonProperty("properties", NullValueHandling = NullValueHandling.Ignore)]
        public List<Property> Properties { get; set; }

        [JsonProperty("place", NullValueHandling = NullValueHandling.Ignore)]
        public Place Place { get; set; }
    }
}