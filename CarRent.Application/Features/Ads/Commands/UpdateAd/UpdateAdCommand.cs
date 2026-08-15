using System;
using System.Collections.Generic;
using System.Text;
using CarRent.Application.Abstractions;
using CarRent.Domain.Enums;
using MediatR;

namespace CarRent.Application.Features.Ads.Commands.UpdateAdCommand
{
    public record UpdateAdCommand 
    (
        Guid Id,
        string Title,
        string Description,
        decimal Price,
        AdType AdType


     ) : IRequest<bool>;
}
