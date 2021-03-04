using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Domain;
using API.Models.AppUser;
using API.Options;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;
        private readonly DataContext _dataContext;
        
        public IdentityService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper, JwtSettings jwtSettings, DataContext dataContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _jwtSettings = jwtSettings;
            _dataContext = dataContext;
        }

        public async Task<RegisterResult> RegisterAsync(string username, string email, string password)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);

            var existingUsername = await _userManager.FindByNameAsync(username);
        
            if (existingUser != null)
            {
                return new RegisterResult
                {
                    Errors = new[] {$"User with '{email}' email address already exists."}
                };
            }
            
            if (existingUsername != null)
            {
                return new RegisterResult
                {
                    Errors = new[] {$"User with '{username}' username already exists."}
                };
            }

            var newUser = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = username,
                Email = email,
                RegistrationDate = DateTime.UtcNow
            };
        
            var createdUser = await _userManager.CreateAsync(newUser, password);
            
            if (!createdUser.Succeeded)
            {
                return new RegisterResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }

            return await GenerateTokenForUserAsync(newUser);
        }


        public async Task<RegisterResult> GenerateTokenForUserAsync(AppUser appUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, appUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
                new Claim("Id", appUser.Id)
            };

            var userClaims = await _userManager.GetClaimsAsync(appUser);
            claims.AddRange(userClaims);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = 
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            await _dataContext.SaveChangesAsync();

            return new RegisterResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
            };
        }
        
    }
}