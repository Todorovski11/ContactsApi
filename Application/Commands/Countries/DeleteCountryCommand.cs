using MediatR;
using ContactsApi.Application.Interfaces;
using Application.Interfaces;

namespace ContactsApi.Application.Commands.Countries
{
    public record DeleteCountryCommand(int CountryId) : IRequest<bool>;

    public class DeleteCountryHandler : IRequestHandler<DeleteCountryCommand, bool>
    {
        private readonly ICountryRepository _repository;

        public DeleteCountryHandler(ICountryRepository repository) => _repository = repository;

        public async Task<bool> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteAsync(request.CountryId);
        }
    }
}
