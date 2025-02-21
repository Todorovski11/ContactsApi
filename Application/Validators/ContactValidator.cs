using FluentValidation;
using ContactsApi.Application.Commands.Contacts;

namespace ContactsApi.Application.Validators
{
    public class ContactValidator : AbstractValidator<CreateContactCommand>
    {
        public ContactValidator()
        {
            RuleFor(x => x.ContactName).NotEmpty().WithMessage("Contact Name is required.");
            RuleFor(x => x.CompanyId).GreaterThan(0).WithMessage("Valid Company ID is required.");
            RuleFor(x => x.CountryId).GreaterThan(0).WithMessage("Valid Country ID is required.");
        }
    }
}
