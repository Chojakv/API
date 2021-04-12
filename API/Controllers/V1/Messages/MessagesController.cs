using System.Threading.Tasks;
using API.Contracts.V1;
using API.Extensions;
using API.Models.Messages;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1.Messages
{
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public MessageController(IMessageService messageService, IMapper mapper, IUriService uriService)
        {
            _messageService = messageService;
            _mapper = mapper;
            _uriService = uriService;
        }
    
        [HttpPost(ApiRoutes.Message.Create)]
        public async Task<IActionResult> Create([FromForm] string username, [FromForm]SendMessageModel model)
        {
            var create = await _messageService.SendMessageAsync(HttpContext.GetUserId(), username, model);

            var locationUri = _uriService.GetMessageUri(create.Payload.Id.ToString());

            return Created(locationUri, _mapper.Map<DetailsSentMessageModel>(create.Payload));
        }
    }

}