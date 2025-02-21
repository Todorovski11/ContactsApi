using MediatR;
using ContactsApi.Application.Interfaces;
using ContactsApi.Domain.Entities;
using Application.Interfaces;

namespace ContactsApi.Application.Commands.Countries
{
    public record UpdateCountryCommand(int CountryId, string CountryName) : IRequest<bool>;

    public class UpdateCountryHandler : IRequestHandler<UpdateCountryCommand, bool>
    {
        private readonly ICountryRepository _repository;

        public UpdateCountryHandler(ICountryRepository repository) => _repository = repository;

        public async Task<bool> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            var country = await _repository.GetByIdAsync(request.CountryId);
            if (country == null) return false;

            country.CountryName = request.CountryName;
            return await _repository.UpdateAsync(country);
        }
    }
}
