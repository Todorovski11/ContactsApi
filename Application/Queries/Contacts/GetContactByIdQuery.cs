using MediatR;
using ContactsApi.Application.Interfaces;
using ContactsApi.Domain.Entities;
using Application.Interfaces;
using Domain.Entities;

namespace ContactsApi.Application.Queries.Contacts
{
    public record GetContactByIdQuery(int ContactId) : IRequest<Contact?>;

    public class GetContactByIdHandler : IRequestHandler<GetContactByIdQuery, Contact?>
    {
        private readonly IContactRepository _repository;

        public GetContactByIdHandler(IContactRepository repository) => _repository = repository;

        public async Task<Contact?> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.ContactId);
        }
    }
}
