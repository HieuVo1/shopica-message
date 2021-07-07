using MESSAGE_SERVICE_NET.Services.ConversationServices;
using MESSAGE_SERVICE_NET.ViewModels.Conversations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MESSAGE_SERVICE_NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationsController : ControllerBase
    {

        private readonly IConversationService _conversationService;

        public ConversationsController(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        [HttpGet("GetAllConversations/{accountId}")]
        public async Task<IActionResult> GetAllConversations(int accountId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _conversationService.GetConversations(accountId);

            return Ok(result);
        }

        [HttpPost("CreateConversation")]
        public async Task<IActionResult> CreateConversationAsync(CreateConversationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _conversationService.CreateConversation(request);

            return Ok(result);
        }
    }
}
