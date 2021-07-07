using MESSAGE_SERVICE_NET.Services;
using MESSAGE_SERVICE_NET.Services.MessageServices;
using MESSAGE_SERVICE_NET.ViewModels;
using MESSAGE_SERVICE_NET.ViewModels.MessagesModels;
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
    public class MessagesController : ControllerBase
    {

        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("getMessagesByConversation/{conversationId}")]
        public async Task<IActionResult> GetMessagesByConversationAsync(int conversationId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _messageService.GetMessageByConversation(conversationId);

            return Ok(result);
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage(MessageRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _messageService.SendMessage(request);

            return Ok(result);
        }

        [HttpPatch("ReadMessage/{conversationId}")]
        public async Task<IActionResult> ReadMessage(int conversationId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _messageService.ReadMessage(conversationId);

            return Ok(result);
        }
    }
}
