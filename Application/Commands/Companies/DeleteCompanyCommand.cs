using MediatR;
using ContactsApi.Application.Interfaces;
using Application.Interfaces;

namespace ContactsApi.Application.Commands.Companies
{
    public record DeleteCompanyCommand(int CompanyId) : IRequest<bool>;

    public class DeleteCompanyHandler : IRequestHandler<DeleteCompanyCommand, bool>
    {
        private readonly ICompanyRepository _repository;

        public DeleteCompanyHandler(ICompanyRepository repository) => _repository = repository;

        public async Task<bool> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteAsync(request.CompanyId);
        }
    }
}
