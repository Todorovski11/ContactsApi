using ContactsApi.Application.Interfaces;
using ContactsApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ContactsApi.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ContactsDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(ContactsDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
            await _dbSet.Where(predicate).ToListAsync();

        public async Task<int> CreateAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            var property = entity.GetType().GetProperty("Id") ?? entity.GetType().GetProperty("CompanyId") ?? entity.GetType().GetProperty("ContactId");
            return property != null ? (int)property.GetValue(entity) : 0;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;
            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
