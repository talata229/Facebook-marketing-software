using System;
using System.Collections.Generic;
using System.Text;

namespace Facebook_Chat.Models
{
    public class MessageFirsebase
    {
        public string Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Content { get; set; }
        public DateTime? TimeReceived { get; set; }

        public DateTime? BlockUntil { get; set; }
        public bool? BlockAll { get; set; }
    }
}
