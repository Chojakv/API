using Application.Models.Ad;
using FluentValidation;

namespace API.Validators
{
    public class AdUpdateModelValidator : AbstractValidator<AdUpdateModel>
    {
        public AdUpdateModelValidator()
        {
            RuleFor(x => x.Title)
                .MaximumLength(60);

            RuleFor(x => x.Author)
                .MaximumLength(40);

            RuleFor(x => x.BookName)
                .MaximumLength(60);

            RuleFor(x => x.Content)
                .NotEmpty()
                .MaximumLength(350)
                .MinimumLength(10);

            RuleFor(x => x.Price)
                .NotEmpty()
                .GreaterThan(0);

        }
    }
}