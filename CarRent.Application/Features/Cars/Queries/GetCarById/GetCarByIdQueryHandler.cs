using CarRent.Application.Abstractions;
using CarRent.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.Application.Features.Cars.Queries.GetCarById
{
    public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQuery,CarDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetCarByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CarDto?>Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {
            var car= await _context.Cars.AsNoTracking()
                .FirstOrDefaultAsync(C=>C.Id==request.Id,cancellationToken);
            if (car == null)
            {
                return null;
            }
            return new CarDto(
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
            );
        }
    }
}
