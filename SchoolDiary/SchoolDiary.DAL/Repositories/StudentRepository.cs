using Microsoft.EntityFrameworkCore;
using SchoolDiary.DAL.DataContext;
using SchoolDiary.DAL.Entities;
using SchoolDiary.DAL.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.DAL.Repositories
{
    public class StudentRepository: GenericRepository<Student, SchoolDiaryContext>
    {
        public StudentRepository(SchoolDiaryContext context):base(context)
        {
            
        }

        public async Task<IEnumerable<Student>> GetStudents(string filter, string orderBy, int? page, int? pageSize)
        {
            return await this.Get().AsNoTracking().Filter(filter).OrderBy(orderBy).Paginate(page, pageSize).ToListAsync();
        }
    }
}
