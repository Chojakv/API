using Application.Models.Ad;
using FluentValidation;

namespace API.Validators
{
    public class AdUpdateModelValidator : AbstractValidator<AdUpdateModel>
    {
        public AdUpdateModelValidator()
        {
            RuleFor(x => x.Title)
                .MaximumLength(15);

            RuleFor(x => x.Author)
                .MaximumLength(20);

            RuleFor(x => x.BookName)
                .MaximumLength(25);

            RuleFor(x => x.Content)
                .MaximumLength(200)
                .MinimumLength(5);

            RuleFor(x => x.Price)
                .NotEmpty()
                .GreaterThan(0);

        }
    }
}