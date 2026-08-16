using System;
using MediatR;

namespace CarRent.Application.Features.Favorites.Commands.AddFavorite
{
    public record AddFavoriteCommand(Guid UserId, Guid AdId) : IRequest<bool>;
}