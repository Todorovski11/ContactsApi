using MediatR;
using ContactsApi.Application.Interfaces;
using ContactsApi.Domain.Entities;
using Application.Interfaces;
using Domain.Entities;

namespace ContactsApi.Application.Queries.Contacts
{
    public record GetAllContactsQuery(int? CountryId, int? CompanyId, int Skip, int Take) : IRequest<List<Contact>>;

    public class GetAllContactsHandler : IRequestHandler<GetAllContactsQuery, List<Contact>>
    {
        private readonly IContactRepository _repository;

        public GetAllContactsHandler(IContactRepository repository) => _repository = repository;

        public async Task<List<Contact>> Handle(GetAllContactsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetContactsAsync(request.CountryId, request.CompanyId, request.Skip, request.Take);
        }
    }
}
