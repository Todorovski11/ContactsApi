using MediatR;
using ContactsApi.Application.Interfaces;
using ContactsApi.Domain.Entities;
using Application.Interfaces;
using Domain.Entities;

namespace ContactsApi.Application.Queries.Companies
{
    public record GetAllCompaniesQuery() : IRequest<List<Company>>;

    public class GetAllCompaniesHandler : IRequestHandler<GetAllCompaniesQuery, List<Company>>
    {
        private readonly ICompanyRepository _repository;

        public GetAllCompaniesHandler(ICompanyRepository repository) => _repository = repository;

        public async Task<List<Company>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
