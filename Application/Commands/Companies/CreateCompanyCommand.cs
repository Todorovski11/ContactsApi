using MediatR;
using ContactsApi.Application.Interfaces;
using ContactsApi.Domain.Entities;
using Application.Interfaces;
using Domain.Entities;

namespace ContactsApi.Application.Commands.Companies
{
    public record CreateCompanyCommand(string CompanyName) : IRequest<int>;

    public class CreateCompanyHandler : IRequestHandler<CreateCompanyCommand, int>
    {
        private readonly ICompanyRepository _repository;

        public CreateCompanyHandler(ICompanyRepository repository) => _repository = repository;

        public async Task<int> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = new Company { CompanyName = request.CompanyName };
            return await _repository.CreateAsync(company);
        }
    }
}
