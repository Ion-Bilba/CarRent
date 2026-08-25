using MediatR;
using System;

namespace CarRent.Application.Features.Favorites.Commands.DeleteFavorite;

public record DeleteFavoriteCommand(Guid AdId) : IRequest<bool>;