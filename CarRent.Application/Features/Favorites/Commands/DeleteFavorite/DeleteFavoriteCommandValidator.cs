using FluentValidation;

namespace CarRent.Application.Features.Favorites.Commands.DeleteFavorite
{
    public class DeleteFavoriteCommandValidator : AbstractValidator<DeleteFavoriteCommand>
    {
        public DeleteFavoriteCommandValidator()
        {
            RuleFor(x => x.AdId)
                .NotEmpty().WithMessage("ID-ul anunțului este obligatoriu.");
        }
    }
}