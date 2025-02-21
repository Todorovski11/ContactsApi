using ContactsApi.Domain.Entities;
using System.Threading.Tasks;

namespace ContactsApi.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<bool> CreateAsync(User user);
    }
}
