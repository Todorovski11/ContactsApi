using MediatR;
using ContactsApi.Application.Interfaces;
using ContactsApi.Domain.Entities;
using Application.Interfaces;

namespace ContactsApi.Application.Commands.Companies
{
    public record UpdateCompanyCommand(int CompanyId, string CompanyName) : IRequest<bool>;

    public class UpdateCompanyHandler : IRequestHandler<UpdateCompanyCommand, bool>
    {
        private readonly ICompanyRepository _repository;

        public UpdateCompanyHandler(ICompanyRepository repository) => _repository = repository;

        public async Task<bool> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _repository.GetByIdAsync(request.CompanyId);
            if (company == null) return false;

            company.CompanyName = request.CompanyName;
            return await _repository.UpdateAsync(company);
        }
    }
}
