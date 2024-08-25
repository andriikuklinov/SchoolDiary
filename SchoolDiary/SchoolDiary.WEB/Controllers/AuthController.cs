﻿using AutoMapper;
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
        private readonly IConfiguration _configuration;
        public AuthController(IMapper mapper, IAuthService authService, IConfiguration configuration)
        {
            this._mapper = mapper;
            this._authService = authService;
            this._configuration = configuration;
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
                        var token = await _authService.Login(authDto, _configuration.GetSection("Jwt"));
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
        public async Task<IActionResult> Login(AuthDTO authDto)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var token = await _authService.Login(authDto, _configuration.GetSection("Jwt"));
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
    }
}
