using Microsoft.AspNetCore.Identity;
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
        Task<SignInResult> Login(AuthDTO authDto);
    }
}
