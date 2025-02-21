using MediatR;
using ContactsApi.Application.Interfaces;
using ContactsApi.Domain.Entities;
using Application.Interfaces;
using Domain.Entities;

namespace ContactsApi.Application.Commands.Contacts
{
    public record CreateContactCommand(string ContactName, int CompanyId, int CountryId) : IRequest<int>;

    public class CreateContactHandler : IRequestHandler<CreateContactCommand, int>
    {
        private readonly IContactRepository _repository;

        public CreateContactHandler(IContactRepository repository) => _repository = repository;

        public async Task<int> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var contact = new Contact
            {
                ContactName = request.ContactName,
                CompanyId = request.CompanyId,
                CountryId = request.CountryId
            };
            return await _repository.CreateAsync(contact);
        }
    }
}
