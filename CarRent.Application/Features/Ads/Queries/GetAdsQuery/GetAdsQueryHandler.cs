using CarRent.Application.Abstractions;
using CarRent.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Features.Ads.Queries.GetAdsQuery;

public class GetAdsQueryHandler : IRequestHandler<GetAdsQuery, PagedResult<AdDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAdsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<AdDto>> Handle(GetAdsQuery request, CancellationToken cancellationToken)
    {
        var ad = _context.Ads
            .AsNoTracking()
            .Where(c => c.IsActive);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var term = request.SearchTerm.Trim().ToLower();

            ad = ad.Where(c =>
                EF.Functions.Like(c.Title, $"%{term}%") ||
                EF.Functions.Like(c.Description, $"%{term}%"));
        }

        if (request.MaxPrice.HasValue)
        {
            ad = ad.Where(c => c.Price <= request.MaxPrice.Value);
        }

        if (request.MinPrice.HasValue)
        {
            ad = ad.Where(c => c.Price >= request.MinPrice.Value);
        }

        if (request.AdType.HasValue)
        {
            ad = ad.Where(c => c.AdType == request.AdType.Value);
        }

        if (request.TractionType.HasValue)
        {
            ad = ad.Where(c => c.Car.TractionType == request.TractionType.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Brand))
        {
            ad = ad.Where(c => c.Car.Brand.Contains(request.Brand));
        }

        if (!string.IsNullOrWhiteSpace(request.Model))
        {
            ad = ad.Where(c => c.Car.Model.Contains(request.Model));
        }

        if (request.MinYear.HasValue)
        {
            ad = ad.Where(c => c.Car.Year >= request.MinYear.Value);
        }

        if (request.MaxYear.HasValue)
        {
            ad = ad.Where(c => c.Car.Year <= request.MaxYear.Value);
        }

        if (request.FuelType.HasValue)
        {
            ad = ad.Where(c => c.Car.FuelType == request.FuelType.Value);
        }

        if (request.Transmission.HasValue)
        {
            ad = ad.Where(c => c.Car.Transmission == request.Transmission.Value);
        }

        var totalcount = await ad.CountAsync(cancellationToken);
        var pageNumber = request.PageNumber > 0 ? request.PageNumber : 1;
        var pageSize = request.PageSize > 0 ? request.PageSize : 10;

        var items = await ad
            .OrderByDescending(c => c.UpdatedAt ?? c.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(ad => new AdDto
            {
                Id = ad.Id,
                CarId = ad.CarId,
                UserId = ad.UserId,
                Title = ad.Title,
                Description = ad.Description,
                Price = ad.Price,
                AdType = ad.AdType,
                TractionType = ad.Car.TractionType,
                CarBrand = ad.Car.Brand,
                CarModel = ad.Car.Model,
                CarYear = ad.Car.Year,
                CarColor = ad.Car.Color,
                CarMileage = ad.Car.Mileage,
                CarFuelType = ad.Car.FuelType,
                CarEngineCapacity = ad.Car.EngineCapacity,
                CarTransmission = ad.Car.Transmission,
                IsActive = ad.IsActive,
                Views = ad.Views,
                CreatedAt = ad.CreatedAt,
                MainImageUrl = ad.AdImages
                .OrderBy(i => i.DisplayOrder)
                .Select(i => i.ImageUrl)
                .FirstOrDefault()
            })
            .ToListAsync(cancellationToken);


        return new PagedResult<AdDto>(
    items: items,
    pageNumber: pageNumber,
    pageSize: pageSize,
    totalCount: totalcount
);
    }
}