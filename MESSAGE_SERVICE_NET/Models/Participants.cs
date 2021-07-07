using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MESSAGE_SERVICE_NET.Models
{
    public class Participants: BaseModel
    {
        public int Conversation_id { get; set; }
        public int User_id { get; set; }

        //reference navigation properties
        public Users User { get; set; }
        public Conversations Conversation { get; set; }
    }
}
