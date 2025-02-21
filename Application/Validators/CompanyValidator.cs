using FluentValidation;
using ContactsApi.Application.Commands.Companies;

namespace ContactsApi.Application.Validators
{
    public class CompanyValidator : AbstractValidator<CreateCompanyCommand>
    {
        public CompanyValidator()
        {
            RuleFor(x => x.CompanyName).NotEmpty().WithMessage("Company Name is required.");
        }
    }
}
