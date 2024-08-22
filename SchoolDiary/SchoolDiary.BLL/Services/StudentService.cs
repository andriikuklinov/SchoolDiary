using AutoMapper;
using SchoolDiary.BLL.DTO;
using SchoolDiary.BLL.Exceptions;
using SchoolDiary.BLL.IServices;
using SchoolDiary.DAL.Entities;
using SchoolDiary.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.BLL.Services
{
    public class StudentService: IStudentService
    {
        private readonly IMapper _mapper;
        private readonly StudentRepository _studentRepository;

        public StudentService(IMapper mapper, StudentRepository studentRepository)
        {
            this._mapper = mapper;
            this._studentRepository = studentRepository;
        }

        public async Task<IEnumerable<StudentDTO>> GetStudents(string filter, string orderBy, int? page, int? pageSize)
        {
            var students = await _studentRepository.GetStudents(filter, orderBy, page, pageSize);
            return _mapper.Map<IEnumerable<StudentDTO>>(students);
        }

        public async Task DeleteStudent(int id)
        {
            var targetStudent = await _studentRepository.GetSingleAsync(_=>_.Id == id);
            if (targetStudent != null)
                await _studentRepository.DeleteAsync(targetStudent);
            throw new EntityNotFoundException();
        }
    }
}
