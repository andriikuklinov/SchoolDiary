using Microsoft.EntityFrameworkCore;
using SchoolDiary.DAL.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.DAL.Repositories
{
    public class GenericRepository<T, TContext>: IGenericRepository<T> where T: class where TContext : DbContext
    {
        private readonly TContext _context;

        public GenericRepository(TContext context)
        {
            this._context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var entityEntry = _context.Attach(entity);
            entityEntry.State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            var entityEntry = _context.Attach(entity);
            entityEntry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();

            return entities;
        }

        public async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            await _context.SaveChangesAsync();

            return entities;
        }

        public async Task<IEnumerable<T>> DeleteRangeAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            _context.SaveChanges();

            return entities;
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> filter = null, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(filter, cancellationToken);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> filter = null, CancellationToken cancellation = default)
        {
            if (filter == null)
                return _context.Set<T>().AsQueryable<T>();
            return _context.Set<T>().Where(filter);
        }
    }
}
