using CarRent.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.Application.DTOs
{
    public record CarDto
    (
        Guid Id,
        string Brand,
        string Model,
        int Year,
        string Color,
        int Mileage,
        FuelType FuelType,
        int EngineCapacity,
        Transmission Transmission,
        TractionType TractionType
    );
}
