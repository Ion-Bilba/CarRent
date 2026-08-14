using CarRent.Application.Abstractions;
using CarRent.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CarRent.Application.Features.Ads.Queries;

public class GetAdsQueryHandler : IRequestHandler<GetAdsQuery, PagedResult<AdDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAdsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<AdDto>> Handle(GetAdsQuery request, CancellationToken cancellationToken)
    {
        var ad= _context.Ads
            .AsNoTracking().Where(c => c.IsActive);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var term = request.SearchTerm.Trim().ToLower();

            ad= ad.Where(c=>c.Title.ToLower().Contains(term) || c.Description.ToLower().Contains(term));    

        }

        if(request.MaxPrice.HasValue)
        {
            ad=ad.Where(c=>c.Price<=request.MaxPrice.Value);
        }

        if (request.AdType.HasValue)
        {
            ad=ad.Where(c=>c.AdType==request.AdType.Value);
        }

        if (request.TractionType.HasValue)
        {
            ad = ad.Where(c => c.Car.TractionType == request.TractionType.Value);
        }

        var totalcount = await ad.CountAsync(cancellationToken);

        var items = await ad.OrderByDescending(C => C.UpdatedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
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
                IsActive = ad.IsActive,
                Views = ad.Views,
                CreatedAt = ad.CreatedAt

            }).ToListAsync(cancellationToken);

        return new PagedResult<AdDto>(items, totalcount,request.PageNumber,request.PageSize);
            
    }
}