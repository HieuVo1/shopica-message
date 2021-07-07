using MESSAGE_SERVICE_NET.Models;
using MESSAGE_SERVICE_NET.ViewModels.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MESSAGE_SERVICE_NET.ViewModels.Conversations
{
    public class ConversationView
    {
        public int Id { get; set; }
        public string ConversationTitle { get; set; }
        public string ConversationImage { get; set; }
        public int Receive_id { get; set; }
        public DateTime Created_at { get; set; }
        public MessageView LastMessage { get; set; }
    }
}
