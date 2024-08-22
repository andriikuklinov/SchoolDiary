using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolDiary.BLL.DTO;
using SchoolDiary.BLL.Exceptions;
using SchoolDiary.BLL.IServices;
using SchoolDiary.WEB.Models;

namespace SchoolDiary.WEB.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStudentService _studentService;

        public StudentController(IMapper mapper, IStudentService studentService)
        {
            this._mapper = mapper;
            this._studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents(string? filter, string? orderBy, int? page, int? pageSize)
        {
            try
            {
                return Ok(new ApiResponse<IEnumerable<StudentDTO>>(await _studentService.GetStudents(filter, orderBy, page, pageSize)));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudent(StudentDTO studentDto)
        {
            try
            {
                return Ok(new ApiResponse<StudentDTO>(await _studentService.UpdateStudent(studentDto)));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                await _studentService.DeleteStudent(id);
                return Ok(new ApiResponse<object>(null));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
