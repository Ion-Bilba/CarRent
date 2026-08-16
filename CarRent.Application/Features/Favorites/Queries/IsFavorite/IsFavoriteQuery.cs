using System;
using MediatR;

namespace CarRent.Application.Features.Favorites.Queries.IsFavorite
{
    public record IsFavoriteQuery(Guid UserId, Guid AdId) : IRequest<bool>;
}