using CarRent.Application.DTOs;
using CarRent.Domain.Enums;
using MediatR;

namespace CarRent.Application.Features.Cars.Queries.GetCarsQuery
{
    public record GetCarsQuery(
        string? Brand = null,
        string? Model = null,
        int? MinYear = null,
        int? MaxYear = null,
        string? Color = null,
        Transmission? Transmission = null,
        FuelType? FuelType = null,
        TractionType? TractionType = null
    ) : IRequest<List<CarDto>>;
}