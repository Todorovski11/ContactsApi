using ContactsApi.Infrastructure.Repositories;
using ContactsApi.Infrastructure.Persistence;
using ContactsApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using Xunit;
using Domain.Entities;

namespace ContactsApi.Tests.RepositoriesTests
{
    public class ContactRepositoryTests
    {
        private ContactsDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ContactsDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new ContactsDbContext(options);
            dbContext.Database.EnsureCreated();
            return dbContext;
        }

        [Fact]
        public async Task GetContactsAsync_ShouldReturnFilteredContacts()
        {
            var dbContext = GetDbContext();
            var repository = new ContactRepository(dbContext);

            var company = new Company { CompanyId = 1, CompanyName = "Google" };
            var country = new Country { CountryId = 1, CountryName = "USA" };
            var contact1 = new Contact { ContactId = 1, ContactName = "John Smith", CompanyId = 1, CountryId = 1, Company = company, Country = country };
            var contact2 = new Contact { ContactId = 2, ContactName = "Jane Smith", CompanyId = 2, CountryId = 2 };

            dbContext.Contacts.AddRange(contact1, contact2);
            await dbContext.SaveChangesAsync();

            var result = await repository.GetContactsAsync(1, null, 0, 10);

            result.Should().HaveCount(1);
            result[0].ContactName.Should().Be("John Smith");
        }
    }
}
