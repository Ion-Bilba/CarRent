using CarRent.Application.Abstractions;
using CarRent.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Features.Cars.Queries.GetCarsQuery
{
    public class GetCarsQueryHandler : IRequestHandler<GetCarsQuery, List<CarDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetCarsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CarDto>> Handle(GetCarsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Cars.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Brand))
            {
                query = query.Where(c => c.Brand.Contains(request.Brand));
            }

            if (!string.IsNullOrWhiteSpace(request.Model))
            {
                query = query.Where(c => c.Model.Contains(request.Model));
            }

            if (request.MinYear.HasValue)
            {
                query = query.Where(c => c.Year >= request.MinYear.Value);
            }

            if (request.MaxYear.HasValue)
            {
                query = query.Where(c => c.Year <= request.MaxYear.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.Color))
            {
                query = query.Where(c => c.Color.Contains(request.Color));
            }

            if (request.Transmission.HasValue)
            {
                query = query.Where(c => c.Transmission == request.Transmission.Value);
            }

            if (request.TractionType.HasValue)
            {
                query = query.Where(c => c.TractionType == request.TractionType.Value);
            }

            if (request.FuelType.HasValue)
            {
                query = query.Where(c => c.FuelType == request.FuelType.Value);
            }

            return await query
                .Select(c => new CarDto(
                    c.Id,
                    c.UserId,
                    c.Brand,
                    c.Model,
                    c.Year,
                    c.Color,
                    c.Mileage,
                    c.FuelType,
                    c.EngineCapacity,
                    c.Transmission,
                    c.TractionType
                ))
                .ToListAsync(cancellationToken);
        }
    }
}