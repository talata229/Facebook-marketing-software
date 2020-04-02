using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Common.ModelsDTO.Responses.Message
{
    public class WelcomeRoot
    {
        [JsonProperty("message_threads")]
        public MessageThreads MessageThreads { get; set; }
    }
}
