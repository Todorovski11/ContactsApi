using MediatR;
using ContactsApi.Application.Interfaces;
using Application.Interfaces;

namespace ContactsApi.Application.Queries.Countries
{
    public record GetCompanyStatisticsQuery(int CountryId) : IRequest<Dictionary<string, int>>;

    public class GetCompanyStatisticsHandler : IRequestHandler<GetCompanyStatisticsQuery, Dictionary<string, int>>
    {
        private readonly ICountryRepository _repository;

        public GetCompanyStatisticsHandler(ICountryRepository repository) => _repository = repository;

        public async Task<Dictionary<string, int>> Handle(GetCompanyStatisticsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetCompanyStatisticsByCountryId(request.CountryId);
        }
    }
}
