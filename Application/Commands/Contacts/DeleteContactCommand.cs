using MediatR;
using ContactsApi.Application.Interfaces;
using Application.Interfaces;

namespace ContactsApi.Application.Commands.Contacts
{
    public record DeleteContactCommand(int ContactId) : IRequest<bool>;

    public class DeleteContactHandler : IRequestHandler<DeleteContactCommand, bool>
    {
        private readonly IContactRepository _repository;

        public DeleteContactHandler(IContactRepository repository) => _repository = repository;

        public async Task<bool> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteAsync(request.ContactId);
        }
    }
}
