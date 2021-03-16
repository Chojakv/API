using System;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using API.Contracts.Requests;
using API.Contracts.V1;
using API.Domain;
using API.Models.AppUser;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1.Identity
{
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }


        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody]AppUserRegistrationModel registrationModel)
        {
            var authenticationResponse = await _identityService.RegisterAsync(registrationModel.UserName, registrationModel.Email, registrationModel.Password);

            if (!authenticationResponse.Success)
            {
                return BadRequest(new RegisterResult
                {
                    Errors = authenticationResponse.Errors
                });
            }

            return Ok(new RegisterResult
            {
                Success = true,
                Token = authenticationResponse.Token,
            });
        }

        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] AppUserLoginModel loginModel)
        {
            var authenticationResponse = await _identityService.LoginAsync(loginModel.Email, loginModel.Password);

            if (!authenticationResponse.Success)
            {
                return BadRequest(new LoginResult
                {
                    Errors = authenticationResponse.Errors
                });
            }

            return Ok(new LoginResult
            {
                Token = authenticationResponse.Token,
                Success = true
            });
        }
    }
}