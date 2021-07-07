using MESSAGE_SERVICE_NET.Models;
using MESSAGE_SERVICE_NET.ViewModels;
using MESSAGE_SERVICE_NET.ViewModels.Commons;
using MESSAGE_SERVICE_NET.ViewModels.MessagesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MESSAGE_SERVICE_NET.Services.MessageServices
{
    public interface IMessageService
    {
        public Task<APIResponse<List<Messages>>> GetMessageByConversation(int conversationId);
        public Task<APIResponse<int>> SendMessage(MessageRequest request);
        public Task<APIResponse<bool>> ReadMessage(int conversationId);
    }
}
