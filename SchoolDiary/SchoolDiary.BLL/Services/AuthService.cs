using Microsoft.AspNetCore.Identity;
using SchoolDiary.BLL.DTO;
using SchoolDiary.BLL.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<SignInResult> Login(AuthDTO authDto)
        {
            if (!string.IsNullOrEmpty(authDto.Email) && !string.IsNullOrEmpty(authDto.Password))
            {
                var user = new IdentityUser { Email = authDto.Email, UserName = authDto.Email };
                await _signInManager.PasswordSignInAsync(user, authDto.Password, authDto.RememberMe, false);
            }
            throw new NullReferenceException("Email or password is empty.");
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
    }
}
