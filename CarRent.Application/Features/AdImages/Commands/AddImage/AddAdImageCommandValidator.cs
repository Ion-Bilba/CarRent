using CarRent.Application.Common.Validation;
using FluentValidation;

namespace CarRent.Application.Features.AdImages.Commands.AddAdImage
{
    public class AddAdImageCommandValidator : AbstractValidator<AddAdImageCommand>
    {
        public AddAdImageCommandValidator()
        {
            RuleFor(x => x.AdId)
                .NotEmpty().WithMessage("ID-ul anunțului este obligatoriu.");

            RuleFor(x => x.Request.ImageUrl)
                .NotEmpty().WithMessage("URL-ul imaginii este obligatoriu.")
                .MustBeValidUrl();

            RuleFor(x => x.Request.DisplayOrder)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinea de afișare nu poate fi negativă.");
        }
    }
}