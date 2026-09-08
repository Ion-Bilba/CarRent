using CarRent.Application.Abstractions;
using CarRent.Application.Common.Interfaces;
using CarRent.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Features.Ads.Queries.GetMyAds
{
    public class GetMyAdsQueryHandler : IRequestHandler<GetMyAdsQuery, List<AdDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public GetMyAdsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<List<AdDto>> Handle(GetMyAdsQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException("Utilizatorul nu este autentificat.");

            return await _context.Ads
                .AsNoTracking()
                .Where(a => a.UserId == currentUserId && a.IsActive)
                .OrderByDescending(a => a.UpdatedAt ?? a.CreatedAt)
                .Select(a => new AdDto
                {
                    Id = a.Id,
                    CarId = a.CarId,
                    UserId = a.UserId,
                    Title = a.Title,
                    Description = a.Description,
                    Price = a.Price,
                    AdType = a.AdType,
                    TractionType = a.Car.TractionType,
                    CarBrand = a.Car.Brand,
                    CarModel = a.Car.Model,
                    CarYear = a.Car.Year,
                    CarColor = a.Car.Color,
                    CarMileage = a.Car.Mileage,
                    CarFuelType = a.Car.FuelType,
                    CarEngineCapacity = a.Car.EngineCapacity,
                    CarTransmission = a.Car.Transmission,
                    IsActive = a.IsActive,
                    Views = a.Views,
                    CreatedAt = a.CreatedAt,
                    MainImageUrl = a.AdImages
                    .OrderBy(i => i.DisplayOrder)
                    .Select(i => i.ImageUrl)
                    .FirstOrDefault()
                })
                .ToListAsync(cancellationToken);
        }
    }
}