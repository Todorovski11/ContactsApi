using MediatR;
using ContactsApi.Application.Interfaces;
using ContactsApi.Domain.Entities;
using Application.Interfaces;
using Domain.Entities;

namespace ContactsApi.Application.Commands.Countries
{
    public record CreateCountryCommand(string CountryName) : IRequest<int>;

    public class CreateCountryHandler : IRequestHandler<CreateCountryCommand, int>
    {
        private readonly ICountryRepository _repository;

        public CreateCountryHandler(ICountryRepository repository) => _repository = repository;

        public async Task<int> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            var country = new Country { CountryName = request.CountryName };
            return await _repository.CreateAsync(country);
        }
    }
}
