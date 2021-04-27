using Application.Models.AppUser;
using FluentValidation;

namespace API.Validators
{
    public class AppUserUpdateModelValidator : AbstractValidator<AppUserUpdateModel>
    {
        public AppUserUpdateModelValidator()
        {
            RuleFor(x => x.Name)
                .Matches("^[a-zA-Z]*$")
                .MaximumLength(30);
            
            RuleFor(x => x.Lastname)
                .Matches("^[a-zA-Z]*$")
                .MaximumLength(35);

            RuleFor(x => x.Email)
                .EmailAddress();

            RuleFor(x => x.PhoneNumber)
                .Matches("^[0-9]*$")
                .MinimumLength(9)
                .MaximumLength(9);
        }
    }
}