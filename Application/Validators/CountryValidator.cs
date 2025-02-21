using FluentValidation;
using ContactsApi.Application.Commands.Countries;

namespace ContactsApi.Application.Validators
{
    public class CountryValidator : AbstractValidator<CreateCountryCommand>
    {
        public CountryValidator()
        {
            RuleFor(x => x.CountryName).NotEmpty().WithMessage("Country Name is required.");
        }
    }
}
