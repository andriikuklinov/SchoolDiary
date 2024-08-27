using Microsoft.AspNetCore.Identity;
using SchoolDiary.BLL.Exceptions;
using SchoolDiary.BLL.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RoleService(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
        }
        public async Task<IdentityResult> AddRoleForUser(string userId, List<string> roles)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var allRoles = _roleManager.Roles.ToList();
                    var addedRoles = roles.Except(userRoles);
                    var removedRoles = userRoles.Except(roles);

                    await _userManager.AddToRolesAsync(user, addedRoles);
                    return await _userManager.RemoveFromRolesAsync(user, removedRoles);
                }
                throw new EntityNotFoundException("User not found.");
            }
            throw new NullReferenceException("userId is null.");
        }

        public async Task<IdentityResult> CreateRole(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return await _roleManager.CreateAsync(new IdentityRole(name));
            }
            throw new NullReferenceException("Name is null.");
        }

        public async Task<IdentityResult> DeleteRole(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var role = await _roleManager.FindByIdAsync(id);
                return await _roleManager.DeleteAsync(role);
            }
            throw new NullReferenceException("Id is null.");
        }
    }
}
