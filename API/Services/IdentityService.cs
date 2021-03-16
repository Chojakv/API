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
        private readonly JwtSettings _jwtSettings;
        private readonly DataContext _dataContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        
        public IdentityService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,JwtSettings jwtSettings, DataContext dataContext, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings;
            _dataContext = dataContext;
            _roleManager = roleManager;
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

            await _userManager.AddToRoleAsync(newUser, "User");
            
            var token = await GenerateTokenForUserAsync(newUser);

            return new RegisterResult
            {
                Token = token,
                Success = true
            };
        }

        public async Task<LoginResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
           
            if (user == null)
            {
                return new LoginResult
                {
                    Errors = new[] {"User does not exists."}
                };
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!userHasValidPassword)
            {
                return new LoginResult
                {
                    Errors = new[] {"User password combination is wrong."}
                };
            }

            var token = await GenerateTokenForUserAsync(user);
            
            return new LoginResult
            {
                Token = token,
                Success = true
            };
        }

        private async Task<string> GenerateTokenForUserAsync(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("Username",user.UserName),
                new Claim("Id", user.Id)
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByNameAsync(userRole);
                if(role == null) continue;
                var roleClaims = await _roleManager.GetClaimsAsync(role);
                foreach (var roleClaim in roleClaims)
                {
                    if(claims.Contains(roleClaim))
                        continue;
                    claims.Add(roleClaim);
                }
            }
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = 
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            await _dataContext.SaveChangesAsync();

            return tokenHandler.WriteToken(token);
        }
        
    }
}