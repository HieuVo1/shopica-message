using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MESSAGE_SERVICE_NET.ViewModels.MessagesModels
{
    public class MessageRequest
    {
        public string SenderName { get; set; }
        public string SenderImage { get; set; }
        public int Sender_id { get; set; }
        public string Content { get; set; }
        public int Receive_id { get; set; }
        public int Conversation_id { get; set; }
        public List<string> FileUrls { get; set; }
    }
}
