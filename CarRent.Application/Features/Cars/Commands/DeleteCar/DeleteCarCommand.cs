using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.Application.Features.Cars.Commands.DeleteCar
{
    public record DeleteCarCommand(Guid Id) : IRequest<bool>;
    
}
