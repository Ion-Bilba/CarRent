using MediatR;

namespace CarRent.Application.Features.Favorites.Commands.AddFavorite;

public record AddFavoriteCommand(Guid AdId) : IRequest<bool>;