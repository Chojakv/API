using Application.Models.Ad;
using FluentValidation;

namespace API.Validators
{
    public class AdCreationModelValidator : AbstractValidator<AdCreationModel>
    { 
        
        public AdCreationModelValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(15)
                .MinimumLength(5);

            RuleFor(x => x.Author)
                .NotEmpty()
                .MaximumLength(20)
                .MinimumLength(5);

            RuleFor(x => x.BookName)
                .NotEmpty()
                .MaximumLength(25)
                .MinimumLength(5);

            RuleFor(x => x.Content)
                .NotEmpty()
                .MaximumLength(200)
                .MinimumLength(5);

            RuleFor(x => x.Price)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);
        }
    }
}