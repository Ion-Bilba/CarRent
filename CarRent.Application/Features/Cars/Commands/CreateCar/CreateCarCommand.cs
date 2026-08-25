using CarRent.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.Application.Features.Cars.Commands.CreatedCar
{
    public record CreateCarCommand
    (
       
        string Brand,
        string Model,
        int Year,
        string Color,
        int Mileage,
        FuelType FuelType,
        int EngineCapacity,
        Transmission Transmission,
        TractionType TractionType



        ) : IRequest<Guid>;
}