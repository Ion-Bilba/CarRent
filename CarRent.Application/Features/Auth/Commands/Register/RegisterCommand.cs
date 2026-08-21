using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.Application.Features.Auth.Commands.Register
{
    public record RegisterCommand
    ( 
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string? PhoneNumber = null
        ) : IRequest<Guid>;
}
