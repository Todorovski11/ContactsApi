using MediatR;
using ContactsApi.Application.Interfaces;
using ContactsApi.Domain.Entities;
using Application.Interfaces;
using Domain.Entities;

namespace ContactsApi.Application.Queries.Countries
{
    public record GetAllCountriesQuery() : IRequest<List<Country>>;

    public class GetAllCountriesHandler : IRequestHandler<GetAllCountriesQuery, List<Country>>
    {
        private readonly ICountryRepository _repository;

        public GetAllCountriesHandler(ICountryRepository repository) => _repository = repository;

        public async Task<List<Country>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
