using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ContactsApi.Application.Queries.Contacts;
using ContactsApi.Application.Interfaces;
using ContactsApi.Domain.Entities;
using FluentAssertions;
using Application.Interfaces;
using Domain.Entities;

namespace ContactsApi.Tests.Handlers
{
    public class GetAllContactsHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnContacts()
        {
            var mockRepo = new Mock<IContactRepository>();
            var contacts = new List<Contact>
            {
                new Contact { ContactId = 1, ContactName = "John Smith", CompanyId = 1, CountryId = 1 },
                new Contact { ContactId = 2, ContactName = "Jane Smith", CompanyId = 1, CountryId = 2 }
            };

            mockRepo.Setup(repo => repo.GetContactsAsync(null, null, 0, 10))
                .ReturnsAsync(contacts);

            var handler = new GetAllContactsHandler(mockRepo.Object);
            var query = new GetAllContactsQuery(null, null, 0, 10);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().HaveCount(2);
            result[0].ContactName.Should().Be("John Smith");
            result[1].ContactName.Should().Be("Jane Smith");
        }
    }
}
