using CarRent.Application.Abstractions;
using CarRent.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Features.Ads.Queries;

public class GetAdsQueryHandler : IRequestHandler<GetAdsQuery, List<AdDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAdsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<AdDto>> Handle(GetAdsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Ads
            .AsNoTracking() 
            .Select(ad => new AdDto
            {
                Id = ad.Id,
                CarId = ad.CarId,
                UserId = ad.UserId,
                Title = ad.Title,
                Description = ad.Description,
                Price = ad.Price,
                AdType = ad.AdType,
                IsActive=ad.IsActive,
                Views=ad.Views,
                CreatedAt=ad.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}