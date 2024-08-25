using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SchoolDiary.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.BLL.IServices
{
    public interface IAuthService
    {
        Task<IdentityResult> Register(AuthDTO authDto);
        Task<string> Login(AuthDTO authDto, IConfigurationSection configuration);
        Task Logout();
        Task<IdentityResult> ResetPassword(ResetPasswordDTO resetPasswordModel);
        Task<string> ForgotPassword(ForgotPasswordDTO forgotPassfordDto);
        Task<IdentityResult> EditUser(UserDto userDto);
        Task<IdentityResult> DeleteUser(string id);
    }
}
