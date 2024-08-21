using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.DAL.Repositories.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetSingleAsync(Expression<Func<T, bool>> filter = null, CancellationToken cancellationToken = default);
        IQueryable<T> Get(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities);
        Task<IEnumerable<T>> DeleteRangeAsync(IEnumerable<T> entities);
    }
}
