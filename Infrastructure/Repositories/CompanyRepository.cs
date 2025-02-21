using Application.Interfaces;
using ContactsApi.Application.Interfaces;
using ContactsApi.Infrastructure.Persistence;
using Domain.Entities;

namespace ContactsApi.Infrastructure.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(ContactsDbContext context) : base(context) { }
    }
}
