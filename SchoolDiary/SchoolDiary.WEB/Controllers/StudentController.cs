using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolDiary.BLL.DTO;
using SchoolDiary.BLL.IServices;

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
        public async Task<IEnumerable<StudentDTO>> GetStudents(string? filter, string? orderBy, int? page, int? pageSize)
        {
            return await _studentService.GetStudents(filter, orderBy, page, pageSize);
        }
    }
}
