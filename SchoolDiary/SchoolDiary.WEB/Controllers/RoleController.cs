using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolDiary.BLL.IServices;
using SchoolDiary.WEB.Models;

namespace SchoolDiary.WEB.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            this._roleService = roleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromForm]string name)
        {
            try
            {
                if(!string.IsNullOrEmpty(name))
                {
                   var result = await _roleService.CreateRole(name);
                    if (result.Succeeded)
                    {
                        return Ok(new ApiResponse<IdentityResult>(result));
                    }
                    return BadRequest(new ApiResponse<IdentityResult>());
                }
                return BadRequest(new ApiResponse<IdentityResult>());
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRole([FromForm]string id)
        {
            try
            {
                var result = await _roleService.DeleteRole(id);
                if (result.Succeeded)
                {
                    return Ok(new ApiResponse<IdentityResult>(result));
                }
                return BadRequest(new ApiResponse<IdentityResult>());
            }
            catch(NullReferenceException ex)
            {
                return BadRequest(new ApiResponse<object>(ex));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddRoleToUser(AddUserToRoleModel addUserToRoleModel)
        {
            try
            {
                var result = await _roleService.AddRoleForUser(addUserToRoleModel.UserId, addUserToRoleModel.Roles);
                if (result.Succeeded)
                {
                    return Ok(new ApiResponse<IdentityResult>(result));
                }
                return BadRequest(new ApiResponse<IdentityResult>());
            }
            catch(NullReferenceException ex)
            {
                return BadRequest(new ApiResponse<object>(ex));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
