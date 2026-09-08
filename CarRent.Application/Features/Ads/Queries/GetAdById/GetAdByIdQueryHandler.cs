using CarRent.Application.Abstractions;
using CarRent.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Features.Ads.Queries.GetAdById
{
    public class GetAdByIdQueryHandler : IRequestHandler<GetAdByIdQuery, AdDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetAdByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<AdDto?> Handle(GetAdByIdQuery request, CancellationToken cancellationToken)
        {
            var ad = await _context.Ads
     .Include(c => c.Car)
     .Include(c => c.AdImages)
     .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (ad == null)
            {
                return null;
            }
            ad.Views++;
            await _context.SaveChangesAsync(cancellationToken);

            var orderedImages = ad.AdImages.OrderBy(i => i.DisplayOrder).ToList();

            return new AdDto
            {
                Id = ad.Id,
                CarId = ad.CarId,
                UserId = ad.UserId,
                Title = ad.Title,
                Description = ad.Description,
                Price = ad.Price,
                AdType = ad.AdType,
                IsActive = ad.IsActive,
                Views = ad.Views,
                CreatedAt = ad.CreatedAt,
                TractionType = ad.Car.TractionType,
                CarBrand = ad.Car.Brand,
                CarModel = ad.Car.Model,
                CarYear = ad.Car.Year,
                CarColor = ad.Car.Color,
                CarMileage = ad.Car.Mileage,
                CarFuelType = ad.Car.FuelType,
                CarEngineCapacity = ad.Car.EngineCapacity,
                CarTransmission = ad.Car.Transmission,
                MainImageUrl = orderedImages.FirstOrDefault()?.ImageUrl,
                AdImages = orderedImages
                .Select(img => new AdImageDto(img.Id, img.AdId, img.ImageUrl, img.DisplayOrder))
                .ToList()
            };
        }
    }
}