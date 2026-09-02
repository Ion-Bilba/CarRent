using CarRent.Application.Abstractions;
using CarRent.Application.Common.Interfaces;
using CarRent.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Features.Cars.Queries.GetMyCars
{
    public class GetMyCarsQueryHandler : IRequestHandler<GetMyCarsQuery, List<CarDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public GetMyCarsQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<List<CarDto>> Handle(
            GetMyCarsQuery request,
            CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException(
                    "Utilizatorul nu este autentificat.");

            var cars = await _context.Cars
                .AsNoTracking()
                .Where(c => c.UserId == currentUserId)
                .ToListAsync(cancellationToken);

            return cars.Select(car => new CarDto(
                car.Id,
                car.UserId,
                car.Brand,
                car.Model,
                car.Year,
                car.Color,
                car.Mileage,
                car.FuelType,
                car.EngineCapacity,
                car.Transmission,
                car.TractionType
            )).ToList();
        }
    }
}