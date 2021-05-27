using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models.Ad;
using Application.Models.AppUser;
using Application.Models.Messages;
using AutoMapper;
using Contracts.Contracts.V1;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace API.Controllers.V1.Users
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAdService _adService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMessageService _messageService;
        private readonly IConfiguration _configuration;

        public UsersController(IUserService userService, IMapper mapper, IAdService adService, IMessageService messageService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _userService = userService;
            _mapper = mapper;
            _adService = adService;
            _messageService = messageService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        /// <summary>
        ///  Returns specified user from the database
        /// </summary>
        [HttpGet(ApiRoutes.Users.Get)]
        public async Task<IActionResult> GetUserByName([FromRoute]string username)
        {
            var user = await _userService.GetUserByNameAsync(username);
            if (user == null)
            {
                return NotFound("Such user does not exists.");
            }

            return Ok(_mapper.Map<AppUserDetailsModel>(user));
        }
        
        /// <summary>
        ///  Returns all ads created by specified user
        /// </summary>
        [HttpGet(ApiRoutes.Users.Ads.GetAll)]
        public async Task<IActionResult> GetUserAds(string username)
        {
            var ads = await _adService.GetUserAdsAsync(username);

            if (ads.Any())
            {
                return Ok((_mapper.Map<IEnumerable<AdDetailsModel>>(ads)));
            }

            return NotFound("This user does not have any ads");
        }

        /// <summary>
        ///  Updates user acc
        /// </summary>
        [HttpPatch(ApiRoutes.Users.Update)]
        public async Task<IActionResult> Update(string username, [FromForm] AppUserUpdateModel model)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest();
            }

            if (ActionResult(username, out var unauthorized)) return unauthorized;

            var user = await _userService.GetUserByNameAsync(username);
            if (user == null)
            {
                return NotFound("Such user does not exists.");
            }
            
            await _userService.UpdateUserAsync(user, model);
            
            return Ok();
        }
        /// <summary>
        ///  Upload/update user avatar
        /// </summary>
        [HttpPost(ApiRoutes.Users.UploadAvatar)]
        public async Task<IActionResult> UploadAvatar(string username, [FromForm] AppUserAvatarModel image)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest();
            }
            if (ActionResult(username, out var unauthorized)) return unauthorized;
            
            var user = await _userService.GetUserByNameAsync(username);
            if (user == null)
            {
                return NotFound("Such user does not exists.");
            }

            var avatarUrl =await _userService.UploadAvatar(username, image);
            
            return Ok(avatarUrl);
        }
        
        /// <summary>
        ///  Returns all sent messages for specified user
        /// </summary>
        [HttpGet(ApiRoutes.Users.Messages.GetAllSent)]
        public async Task<IActionResult> GetUserSentMessages(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest();
            }

            if (ActionResult(username, out var unauthorized)) return unauthorized;
            
            var messages = await _messageService.GetUserSentMessages(username);
            
            return Ok((_mapper.Map<IEnumerable<DetailsSentMessageModel>>(messages)));
        }

        /// <summary>
        ///  Returns all received messages for specified user
        /// </summary>
        [HttpGet(ApiRoutes.Users.Messages.GetAllReceived)]
        public async Task<IActionResult> GetUserReceivedMessages(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest();
            }
            
            if (ActionResult(username, out var unauthorized)) return unauthorized;

            var messages = await _messageService.GetUserReceivedMessages(username);

           
            return Ok((_mapper.Map<IEnumerable<DetailsReceivedMessageModel>>(messages)));

        }

        /// <summary>
        ///  Returns number of new messages for specified user
        /// </summary>
        [HttpGet(ApiRoutes.Users.Messages.NewMessagesCount)]
        public async Task<IActionResult> NewMessagesCount(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest();
            }
            
            if (ActionResult(username, out var unauthorized)) return unauthorized;
        
            var newMessages = await _messageService.NewMessagesCount(username);
        
            return Ok(newMessages);
        }

        private bool ActionResult(string username, out IActionResult unauthorized)
        {
            var name = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Username")?.Value;

            if (name != username)
            {
                {
                    unauthorized = Unauthorized();
                    return true;
                }
            }

            unauthorized = null;
            return false;
        }
    }
}