using MESSAGE_SERVICE_NET.Models;
using MESSAGE_SERVICE_NET.ViewModels;
using MESSAGE_SERVICE_NET.ViewModels.Commons;
using MESSAGE_SERVICE_NET.ViewModels.Conversations;
using MESSAGE_SERVICE_NET.ViewModels.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MESSAGE_SERVICE_NET.Services.ConversationServices
{
    public class ConversationService: IConversationService
    {
        private readonly MessageDbContext _context;

        public ConversationService(MessageDbContext context)
        {
            _context = context;
        }

        public async Task<APIResponse<int>> CreateConversation(CreateConversationRequest request)
        {
            Users customer = await _context.Users.Include(x => x.Participants)
                .FirstOrDefaultAsync(x => x.Id == request.Customer.Id);

            Users seller = await _context.Users.Include(x => x.Participants)
                .FirstOrDefaultAsync(x => x.Id == request.Seller.Id);

            if (customer == null)
            {
                customer = new Users()
                {
                    Id = request.Customer.Id,
                    FullName = request.Customer.FullName,
                    Email = request.Customer.Email,
                    Phone = request.Customer.Phone,
                    Image = request.Customer.Image,
                    Participants = new List<Participants>()
                };
            }

            if (seller == null)
            {
                seller = new Users()
                {
                    Id = request.Seller.Id,
                    FullName = request.Seller.FullName,
                    Email = request.Seller.Email,
                    Phone = request.Seller.Phone,
                    Image = request.Seller.Image,
                    Participants = new List<Participants>()
                };
            }

            var commonConversationIds = customer?.Participants.Join(
                seller?.Participants,
                c => c.Conversation_id,
                s => s.Conversation_id,
                (c, s) => c.Conversation_id
                ).ToList();

            if (commonConversationIds?.Count > 0)
            {
                return new APISuccessResponse<int>(commonConversationIds.FirstOrDefault(), StatusCodes.Status200OK);
            }

            var conversation = new Conversations()
            {
                Title = "New conversation",
                Creator_id = request.Customer.Id,
                Participants = new List<Participants>()
                {
                    new Participants() {  User = customer},
                    new Participants() {  User = seller},
                }
            };


            _context.Conversations.Add(conversation);

            await _context.SaveChangesAsync();

            return new APISuccessResponse<int>(conversation.Id, StatusCodes.Status201Created);
        }

        public async Task<APIResponse<List<ConversationView>>> GetConversations(int userId)
        {
            var conversations = await _context.Conversations.Include(x => x.Participants)
                               .ThenInclude(p => p.User)
                            .Include(x=> x.Messages)
                            .Where(x => x.Participants.Any(x=>x.User_id == userId))
                            .ToListAsync();


            var result = conversations.Select(c =>
            {
                var otherPeople = c.Participants.FirstOrDefault(x => x.User_id != userId).User;
                return new ConversationView()
                {
                    Id = c.Id,
                    ConversationTitle = otherPeople?.FullName,
                    ConversationImage = otherPeople?.Image,
                    Receive_id = otherPeople.Id,
                    Created_at = c.Created_at,
                    LastMessage = c.Messages.OrderByDescending(x => x.Created_at).Select(x => new MessageView()
                    {
                        Id = x.Id,
                        Content = x.Content,
                        Sender_id = x.Sender_id,
                        IsRead = x.IsRead,
                        Created_at = x.Created_at
                    }).FirstOrDefault()
                };
            }).OrderByDescending(x=> x.LastMessage?.Created_at).ToList();

            return new APISuccessResponse<List<ConversationView>>(result, StatusCodes.Status200OK);
        }
    }
}
