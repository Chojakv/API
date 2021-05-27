using System;
using System.Threading.Tasks;
using API.Extensions;
using Application.Interfaces;
using Application.Models.Messages;
using AutoMapper;
using Contracts.Contracts.V1;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1.Messages
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IUriService _uriService;
        private readonly IMapper _mapper;

        public MessageController(IMessageService messageService, IUriService uriService, IMapper mapper)
        {
            _messageService = messageService;
            _uriService = uriService;
            _mapper = mapper;
        }
        
        /// <summary>
        ///  Creates new message
        /// </summary>
        [Authorize]
        [HttpPost(ApiRoutes.Message.Create)]
        public async Task<IActionResult> Create([FromForm]SendMessageModel model)
        {
            var create = await _messageService.SendMessageAsync(HttpContext.GetUserId(),model);
            
            return Ok(_mapper.Map<DetailsSentMessageModel>(create.Payload)); 
        }

        
        [HttpGet(ApiRoutes.Message.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid messageId)
        {
            var message = await _messageService.GetById(messageId);
            
            if (message == null)
            { 
                return NotFound("Such message does not exists.");
            }
            return Ok(message);
        }
        
    }

}