using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MESSAGE_SERVICE_NET.Models
{
    public class Messages : BaseModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Conversation_id { get; set; }
        public int Sender_id { get; set; }
        public bool IsRead { get; set; }

        //reference navigation properties
        public Conversations Conversation { get; set; }

        //collection navigation properties
        public ICollection<Attachments> Attachments { get; set; }
    }
}
