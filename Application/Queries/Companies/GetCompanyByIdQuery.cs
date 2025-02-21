using MediatR;
using ContactsApi.Application.Interfaces;
using ContactsApi.Domain.Entities;
using Application.Interfaces;
using Domain.Entities;

namespace ContactsApi.Application.Queries.Companies
{
    public record GetCompanyByIdQuery(int CompanyId) : IRequest<Company?>;

    public class GetCompanyByIdHandler : IRequestHandler<GetCompanyByIdQuery, Company?>
    {
        private readonly ICompanyRepository _repository;

        public GetCompanyByIdHandler(ICompanyRepository repository) => _repository = repository;

        public async Task<Company?> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.CompanyId);
        }
    }
}
