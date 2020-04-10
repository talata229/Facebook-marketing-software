using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Facebook_Chat.Models.Response.CovidVN
{
    public class Data
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("data")]
        public List<DataDatum> DataData { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}