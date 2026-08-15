using CarRent.Domain.Enums;

namespace CarRent.Application.Features.Cars.Commands.UpdateCar
{
    public record UpdateCarRequest
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
    );
}