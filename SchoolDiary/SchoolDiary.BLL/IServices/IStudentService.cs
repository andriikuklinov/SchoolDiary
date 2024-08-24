using SchoolDiary.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.BLL.IServices
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDTO>> GetStudents(string filter, string orderBy, int? page, int? pageSize);
        Task<StudentDTO> CreateStudent(StudentDTO studentDto);
        Task<StudentDTO> DeleteStudent(int id);
        Task<StudentDTO> UpdateStudent(StudentDTO studentDTO);
    }
}
