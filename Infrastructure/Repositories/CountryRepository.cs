using Application.Interfaces;
using ContactsApi.Application.Interfaces;
using ContactsApi.Infrastructure.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactsApi.Infrastructure.Repositories
{
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        private readonly ContactsDbContext _context;

        public CountryRepository(ContactsDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, int>> GetCompanyStatisticsByCountryId(int countryId)
        {
            var country = await _context.Countries
                .Include(c => c.Contacts)
                .ThenInclude(c => c.Company)
                .FirstOrDefaultAsync(c => c.CountryId == countryId);

            if (country == null) return new Dictionary<string, int>();

            return country.Contacts
                .Where(c => c.Company != null)
                .GroupBy(c => c.Company!.CompanyName)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
