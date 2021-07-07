using MESSAGE_SERVICE_NET.Models;
using MESSAGE_SERVICE_NET.ViewModels.Commons;
using MESSAGE_SERVICE_NET.ViewModels.Conversations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MESSAGE_SERVICE_NET.Services.ConversationServices
{
    public interface IConversationService
    {
        public Task<APIResponse<int>> CreateConversation(CreateConversationRequest  request);
        public Task<APIResponse<List<ConversationView>>> GetConversations(int userId);
    }
}
