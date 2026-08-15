using FluentValidation;

namespace CarRent.Application.Features.AdImages.Commands.DeleteAdImage
{
    public class DeleteAdImageCommandValidator : AbstractValidator<DeleteAdImageCommand>
    {
        public DeleteAdImageCommandValidator()
        {
            RuleFor(x => x.AdId)
                .NotEmpty().WithMessage("ID-ul anunțului este obligatoriu.");

            RuleFor(x => x.ImageId)
                .NotEmpty().WithMessage("ID-ul imaginii este obligatoriu.");
        }
    }
}