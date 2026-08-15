using CarRent.Application.Abstractions;
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
       public CreateCarCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateCarCommand request,CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .AnyAsync(c => c.Id == request.UserId,cancellationToken);

            if(!user)
            {
                throw new KeyNotFoundException($"Userul cu ID-ul {request.UserId} nu există.");
            }

            var car = new Car
            { 
                Id=Guid.NewGuid(),
                UserId = request.UserId,
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