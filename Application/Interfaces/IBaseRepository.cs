using System.Linq.Expressions;

namespace ContactsApi.Application.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<int> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
