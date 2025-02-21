using ContactsApi.Application.Interfaces;
using ContactsApi.Domain.Entities;
using ContactsApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ContactsApi.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ContactsDbContext _context;

        public UserRepository(ContactsDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<bool> CreateAsync(User user)
        {
            _context.Users.Add(user);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
