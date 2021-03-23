using API.Models.Ad;
using FluentValidation;

namespace API.Validators
{
    public class AdCreationModelValidator : AbstractValidator<AdCreationModel>
    {
        public AdCreationModelValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(60)
                .MinimumLength(10);

            RuleFor(x => x.Author)
                .NotEmpty()
                .MaximumLength(40)
                .MinimumLength(3);

            RuleFor(x => x.BookName)
                .NotEmpty()
                .MaximumLength(60)
                .MinimumLength(3);

            RuleFor(x => x.Content)
                .NotEmpty()
                .MaximumLength(350)
                .MinimumLength(10);

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0);
        }
    }
}