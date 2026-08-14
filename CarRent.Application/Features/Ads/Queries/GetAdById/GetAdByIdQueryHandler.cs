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
     .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (ad==null)
            {
                return null;
            }
            ad.Views++;
            await _context.SaveChangesAsync(cancellationToken);

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
                TractionType=ad.Car.TractionType
            };



        }
    }
}
