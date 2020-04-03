using System;
using Newtonsoft.Json;

namespace Common.ModelsDTO.Responses.Home
{
    public class Paging
    {
        [JsonProperty("cursors")]
        public Cursors Cursors { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }
    }
}