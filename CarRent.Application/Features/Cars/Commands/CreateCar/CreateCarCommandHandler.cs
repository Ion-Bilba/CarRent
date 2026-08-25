using CarRent.Application.Abstractions;
using CarRent.Application.Common.Interfaces;
using CarRent.Domain.Entities;
using CarRent.Domain.Enums;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.Application.Features.Cars.Commands.CreatedCar
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand ,Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
       public CreateCarCommandHandler(IApplicationDbContext context,ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Guid> Handle(CreateCarCommand request,CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.UserId
            ?? throw new UnauthorizedAccessException("Utilizatorul nu este autentificat.");

            var car = new Car
            { 
                Id=Guid.NewGuid(),
                UserId = currentUserId,
                Brand =request.Brand,
                Model=request.Model,
                Year=request.Year,
                Color= request.Color,
                Mileage=request.Mileage,
                FuelType=request.FuelType,
                EngineCapacity=request.EngineCapacity,
                Transmission = request.Transmission,
                TractionType=request.TractionType
            };
           _context.Cars.Add(car);
            await _context.SaveChangesAsync(cancellationToken);
            return car.Id;

        }

    }
}