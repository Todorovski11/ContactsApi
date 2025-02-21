using MediatR;
using ContactsApi.Application.Interfaces;
using ContactsApi.Domain.Entities;
using Application.Interfaces;

namespace ContactsApi.Application.Commands.Contacts
{
    public record UpdateContactCommand(int ContactId, string ContactName, int CompanyId, int CountryId) : IRequest<bool>;

    public class UpdateContactHandler : IRequestHandler<UpdateContactCommand, bool>
    {
        private readonly IContactRepository _repository;

        public UpdateContactHandler(IContactRepository repository) => _repository = repository;

        public async Task<bool> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _repository.GetByIdAsync(request.ContactId);
            if (contact == null) return false;

            contact.ContactName = request.ContactName;
            contact.CompanyId = request.CompanyId;
            contact.CountryId = request.CountryId;

            return await _repository.UpdateAsync(contact);
        }
    }
}
