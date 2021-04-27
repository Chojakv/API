using Application.Models.AppUser;
using FluentValidation;

namespace API.Validators
{
    public class AppUserRegistrationModelValidator : AbstractValidator<AppUserRegistrationModel>
    {
        public AppUserRegistrationModelValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .Matches("^[a-zA-Z0-9_]*$")
                .MaximumLength(20)
                .MinimumLength(3);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty();

        }
    }
}