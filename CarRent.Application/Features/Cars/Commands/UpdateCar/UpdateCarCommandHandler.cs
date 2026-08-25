using CarRent.Application.Abstractions;
using CarRent.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CarRent.Application.Features.Cars.Commands.UpdateCar
{
    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public UpdateCarCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
          
            var currentUserId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException("Utilizatorul nu este autentificat.");

          
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            if (car == null)
            {
                throw new KeyNotFoundException($"Mașina cu ID-ul {request.Id} nu a fost găsită.");
            }

           
            if (car.UserId != currentUserId && !_currentUserService.IsAdmin)
            {
                throw new UnauthorizedAccessException("Nu poți modifica o mașină care nu îți aparține.");
            }

            
            car.Brand = request.Brand;
            car.Model = request.Model;
            car.Year = request.Year;
            car.Color = request.Color;
            car.Mileage = request.Mileage;
            car.FuelType = request.FuelType;
            car.EngineCapacity = request.EngineCapacity;
            car.Transmission = request.Transmission;
            car.TractionType = request.TractionType;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}