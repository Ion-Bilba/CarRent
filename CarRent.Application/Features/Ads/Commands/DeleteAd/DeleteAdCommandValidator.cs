using FluentValidation;

namespace CarRent.Application.Features.Ads.Commands.DeleteAd;

public class DeleteAdCommandValidator : AbstractValidator<DeleteAdCommand>
{
    public DeleteAdCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID-ul anunțului este obligatoriu.");
    }
}