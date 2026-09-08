using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CarRent.Application.Abstractions;
using CarRent.Application.Common.Interfaces;
using CarRent.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Features.Favorites.Queries.GetMyFavorites
{
    public class GetMyFavoritesQueryHandler : IRequestHandler<GetMyFavoritesQuery, List<AdDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public GetMyFavoritesQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<List<AdDto>> Handle(GetMyFavoritesQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException("Utilizatorul nu este autentificat.");

            return await _context.Favorites
                .AsNoTracking()
                .Where(f => f.UserId == currentUserId)
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
                    CarBrand = f.Ad.Car.Brand,
                    CarModel = f.Ad.Car.Model,
                    CarYear = f.Ad.Car.Year,
                    CarColor = f.Ad.Car.Color,
                    CarMileage = f.Ad.Car.Mileage,
                    CarFuelType = f.Ad.Car.FuelType,
                    CarEngineCapacity = f.Ad.Car.EngineCapacity,
                    CarTransmission = f.Ad.Car.Transmission,
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