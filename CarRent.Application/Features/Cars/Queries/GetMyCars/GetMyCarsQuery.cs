using CarRent.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.Application.Features.Cars.Queries.GetMyCars
{
    public record GetMyCarsQuery : IRequest<List<CarDto>>;
    
}
