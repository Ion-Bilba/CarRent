using CarRent.Application.DTOs;
using MediatR;


namespace CarRent.Application.Features.Cars.Queries.GetCarById
{
    public record GetCarByIdQuery(Guid Id) : IRequest<CarDto?>;
    
}
