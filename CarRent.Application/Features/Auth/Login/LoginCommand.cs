using CarRent.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.Application.Features.Auth.Login
{
    public record LoginCommand
    (
      string Email,
      string Password
        
    ):IRequest<AuthResponseDto>;
}
