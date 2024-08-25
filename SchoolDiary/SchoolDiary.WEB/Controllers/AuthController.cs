using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Linq;
using SchoolDiary.BLL.DTO;
using SchoolDiary.BLL.Exceptions;
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
        private readonly IConfiguration _configuration;
        public AuthController(IMapper mapper, IAuthService authService, IConfiguration configuration)
        {
            this._mapper = mapper;
            this._authService = authService;
            this._configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Register(AuthModel authModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var registerResult = await _authService.Register(_mapper.Map<AuthDTO>(authModel));
                    if (registerResult.Succeeded)
                    {
                        var token = await _authService.Login(_mapper.Map<AuthDTO>(authModel), _configuration.GetSection("Jwt"));
                        if (!string.IsNullOrEmpty(token))
                        {
                            return Ok(new ApiResponse<string>());
                        }
                        return BadRequest(new ApiResponse<string>());
                    }
                    return BadRequest(new ApiResponse<IdentityResult>());
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

        [HttpPost]
        public async Task<IActionResult> Login(AuthModel authModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var token = await _authService.Login(_mapper.Map<AuthDTO>(authModel), _configuration.GetSection("Jwt"));
                    if (!string.IsNullOrEmpty(token))
                    {
                        return Ok(new ApiResponse<string>(token));
                    }
                    return BadRequest(new ApiResponse<string>());
                }
                return BadRequest(new ApiResponse<IEnumerable<ModelError>>(ModelState.Values.SelectMany(value => value.Errors)));
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public async Task<IActionResult> ResetPassword(Models.ResetPasswordModel resetPasswordModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _authService.ResetPassword(_mapper.Map<ResetPasswordDTO>(resetPasswordModel));
                    return Ok(new ApiResponse<IdentityResult>(result));
                }
                return BadRequest(new ApiResponse<IEnumerable<ModelError>>(ModelState.Values.SelectMany(value => value.Errors)));
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public async Task<IActionResult> ForgotPassword(Models.ForgotPasswordModel forgotPasswordModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var resultToken = await _authService.ForgotPassword(_mapper.Map<ForgotPasswordDTO>(forgotPasswordModel));
                    return Ok(new ApiResponse<string>(Url.Action(nameof(ResetPassword), "Auth", new { resultToken }, Request.Scheme)))
                }
                return BadRequest(new ApiResponse<IEnumerable<ModelError>>(ModelState.Values.SelectMany(value => value.Errors)));
            }
            catch(EntityNotFoundException ex)
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
