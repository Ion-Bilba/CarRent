using System;
using MediatR;

namespace CarRent.Application.Features.Favorites.Commands.DeleteFavorite
{
    public record DeleteFavoriteCommand(Guid UserId, Guid AdId) : IRequest<bool>;
}