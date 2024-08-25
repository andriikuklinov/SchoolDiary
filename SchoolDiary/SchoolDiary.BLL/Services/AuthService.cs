using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolDiary.BLL.DTO;
using SchoolDiary.BLL.IServices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SchoolDiary.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        public async Task<string> Login(AuthDTO authDto, IConfigurationSection configuration)
        {
            if (!string.IsNullOrEmpty(authDto.Email) && !string.IsNullOrEmpty(authDto.Password))
            {
                var result = await _signInManager.PasswordSignInAsync(authDto.Email, authDto.Password, authDto.RememberMe, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(authDto.Email);
                    var token = GenerateToken(user, configuration);
                    return token;
                }
                return null;
            }
            throw new NullReferenceException("Email or password is empty.");
        }
        private string GenerateToken(IdentityUser user, IConfigurationSection configuration)
        {
            var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Issuer"],
                audience: configuration["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<IdentityResult> Register(AuthDTO authDto)
        {
            if (!string.IsNullOrEmpty(authDto.Email) && !string.IsNullOrEmpty(authDto.Password))
            {
                var user = new IdentityUser { Email = authDto.Email, UserName = authDto.Email };
                return await _userManager.CreateAsync(user, authDto.Password);
            }
            throw new NullReferenceException("Email or password is empty.");
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
