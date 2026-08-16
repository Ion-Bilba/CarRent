using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CarRent.Application.Abstractions;
using CarRent.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Features.Favorites.Queries.GetMyFavorites
{
    public class GetMyFavoritesQueryHandler : IRequestHandler<GetMyFavoritesQuery, List<AdDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetMyFavoritesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AdDto>> Handle(GetMyFavoritesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Favorites
                .AsNoTracking()
                .Where(f => f.UserId == request.UserId)
                .OrderByDescending(f => f.SavedAt)
                .Select(f => new AdDto
                {
                    Id = f.Ad.Id,
                    UserId = f.Ad.UserId,
                    CarId = f.Ad.CarId,
                    Title = f.Ad.Title,
                    Description = f.Ad.Description,
                    Price = f.Ad.Price,
                    AdType = f.Ad.AdType,
                    TractionType = f.Ad.Car.TractionType,
                    IsActive = f.Ad.IsActive,
                    Views = f.Ad.Views,
                    CreatedAt = f.Ad.CreatedAt,
                    MainImageUrl = f.Ad.AdImages
                        .OrderBy(i => i.DisplayOrder)
                        .Select(i => i.ImageUrl)
                        .FirstOrDefault()
                })
                .ToListAsync(cancellationToken);
        }
    }
}