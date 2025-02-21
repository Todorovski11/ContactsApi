using MediatR;
using ContactsApi.Application.Interfaces;
using ContactsApi.Domain.Entities;
using Application.Interfaces;
using Domain.Entities;

namespace ContactsApi.Application.Queries.Countries
{
    public record GetCountryByIdQuery(int CountryId) : IRequest<Country?>;

    public class GetCountryByIdHandler : IRequestHandler<GetCountryByIdQuery, Country?>
    {
        private readonly ICountryRepository _repository;

        public GetCountryByIdHandler(ICountryRepository repository) => _repository = repository;

        public async Task<Country?> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.CountryId);
        }
    }
}
