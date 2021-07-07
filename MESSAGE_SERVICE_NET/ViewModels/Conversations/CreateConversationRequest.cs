using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MESSAGE_SERVICE_NET.ViewModels.Conversations
{
    public class CreateConversationRequest
    {
        public UserRequest Customer { get; set; }
        public UserRequest Seller { get; set; }
    }
}
