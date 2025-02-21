using Xunit;
using FluentValidation.TestHelper;
using ContactsApi.Application.Validators;
using ContactsApi.Application.Commands.Contacts;

namespace ContactsApi.Tests.Validators
{
    public class ContactValidatorTests
    {
        private readonly ContactValidator _validator;

        public ContactValidatorTests()
        {
            _validator = new ContactValidator();
        }

        [Fact]
        public void Should_Have_Error_When_ContactName_Is_Empty()
        {
            var command = new CreateContactCommand("", 1, 1);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ContactName);
        }

        [Fact]
        public void Should_Have_Error_When_CompanyId_Is_Invalid()
        {
            var command = new CreateContactCommand("John Doe", 0, 1);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.CompanyId);
        }

        [Fact]
        public void Should_Have_Error_When_CountryId_Is_Invalid()
        {
            var command = new CreateContactCommand("John Doe", 1, 0);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.CountryId);
        }

        [Fact]
        public void Should_Not_Have_Error_When_All_Fields_Are_Valid()
        {
            var command = new CreateContactCommand("John Doe", 1, 1);
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
