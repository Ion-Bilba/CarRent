using System.Collections.Generic;
using CarRent.Application.DTOs;
using MediatR;

namespace CarRent.Application.Features.Favorites.Queries.GetMyFavorites;

public record GetMyFavoritesQuery : IRequest<List<AdDto>>;