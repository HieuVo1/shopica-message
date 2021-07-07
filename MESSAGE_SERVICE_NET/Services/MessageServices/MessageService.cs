using MESSAGE_SERVICE_NET.Hubs;
using MESSAGE_SERVICE_NET.Models;
using MESSAGE_SERVICE_NET.ViewModels.Commons;
using MESSAGE_SERVICE_NET.ViewModels.Conversations;
using MESSAGE_SERVICE_NET.ViewModels.Messages;
using MESSAGE_SERVICE_NET.ViewModels.MessagesModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MESSAGE_SERVICE_NET.Services.MessageServices
{
    public class MessageService : IMessageService
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly MessageDbContext _context;

        public MessageService(MessageDbContext context, IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
            _context = context;
        }

        public async Task<APIResponse<List<Messages>>> GetMessageByConversation(int conversationId)
        {
            var messages = await _context.Messages.Where(x => x.Conversation_id == conversationId).ToListAsync();

            return new APISuccessResponse<List<Messages>>(messages, StatusCodes.Status200OK);
        }

        public async Task<APIResponse<bool>> ReadMessage(int conversationId)
        {
            var messages = await _context.Messages.Where(x => x.Conversation_id == conversationId).ToListAsync();

            foreach(var message in messages)
            {
                message.IsRead = true;
            }

            await _context.SaveChangesAsync();

            return new APISuccessResponse<bool>(StatusCodes.Status200OK);
        }

        public async Task<APIResponse<int>> SendMessage(MessageRequest request)
        {
            var message = new Messages()
            {
                Content = request.Content,
                Conversation_id = request.Conversation_id,
                Sender_id = request.Sender_id,
                IsRead = false
            };

            if(request.FileUrls?.Count > 0)
            {
              
            }
       
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            var conversation = new ConversationView()
            {
                Id = message.Conversation_id,
                ConversationTitle = request.SenderName,
                ConversationImage = request.SenderImage,
                Receive_id = request.Receive_id,
                Created_at = message.Created_at,
                LastMessage = new MessageView()
                {
                    Id = message.Id,
                    Content = message.Content,
                    Sender_id = message.Sender_id,
                    Created_at = message.Created_at
                }
            };

            await _hubContext.Clients.User(request.Receive_id.ToString()).SendAsync("NewMessage", conversation);

            return new APISuccessResponse<int>(message.Id, StatusCodes.Status201Created);
        }
    }
}
