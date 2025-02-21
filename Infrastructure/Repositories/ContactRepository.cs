using Application.Interfaces;
using ContactsApi.Application.Interfaces;
using ContactsApi.Infrastructure.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactsApi.Infrastructure.Repositories
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        private readonly ContactsDbContext _context;

        public ContactRepository(ContactsDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Contact>> GetContactsAsync(int? countryId, int? companyId, int skip, int take)
        {
            return await _context.Contacts
                .Where(c => (!countryId.HasValue || c.CountryId == countryId.Value) &&
                            (!companyId.HasValue || c.CompanyId == companyId.Value))
                .Skip(skip).Take(take)
                .Include(c => c.Company)
                .Include(c => c.Country)
                .ToListAsync();
        }
    }
}
