using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.Application.Features.Cars.Commands
{
    public record CreateCarCommand
    (
        ) : IRequest<Guid>;
}
