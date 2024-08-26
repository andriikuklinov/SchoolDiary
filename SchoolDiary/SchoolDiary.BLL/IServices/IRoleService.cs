using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.BLL.IServices
{
    public interface IRoleService
    {
        Task<IdentityResult> CreateRole(string name);
        Task<IdentityResult> AddRoleForUser(string userId, List<string> roles);
        Task<IdentityResult> DeleteRole(string id);
    }
}
