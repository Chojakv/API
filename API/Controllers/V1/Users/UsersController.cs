using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Contracts.V1;
using API.Domain;
using API.Extensions;
using API.Models.Ad;
using API.Models.AppUser;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1.Users
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAdService _adService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public UsersController(IUserService userService, IMapper mapper, IAdService adService, UserManager<AppUser> userManager)
        {
            _userService = userService;
            _mapper = mapper;
            _adService = adService;
            _userManager = userManager;
        }

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


        [HttpPatch(ApiRoutes.Users.Update)]
        public async Task<IActionResult> Update(string username, [FromForm] AppUserUpdateModel model)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest();
            }

            var name = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Username")?.Value;
            
            if (name != username)
            {
                return Unauthorized();
            }

            var user = await _userService.GetUserByNameAsync(username);
            if (user == null)
            {
                return NotFound("Such user does not exists.");
            }
            
            await _userService.UpdateUserAsync(user, model);

            return Ok();

        }
        
    }
}