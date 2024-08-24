using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SchoolDiary.BLL.DTO;
using SchoolDiary.BLL.IServices;
using SchoolDiary.WEB.Models;
using Identity = Microsoft.AspNetCore.Identity;

namespace SchoolDiary.WEB.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        public AuthController(IMapper mapper, IAuthService authService)
        {
            this._mapper = mapper;
            this._authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(AuthDTO authDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var registerResult = await _authService.Register(authDto);
                    if (registerResult.Succeeded)
                    {
                        var loginResult = await _authService.Login(authDto);
                        if (loginResult.Succeeded)
                        {
                            return Ok(new ApiResponse<Identity.SignInResult>(loginResult));
                        }
                        return BadRequest(new ApiResponse<Identity.SignInResult>(loginResult));
                    }
                    return BadRequest(new ApiResponse<IdentityResult>(registerResult));
                }
                return BadRequest(new ApiResponse<IEnumerable<ModelError>>(ModelState.Values.SelectMany(value => value.Errors)));
            }
            catch(NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }
    }
}
