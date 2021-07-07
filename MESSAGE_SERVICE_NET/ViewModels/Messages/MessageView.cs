using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MESSAGE_SERVICE_NET.ViewModels.Messages
{
    public class MessageView
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime Created_at { get; set; }
        public int Sender_id { get; set; }

    }
}
