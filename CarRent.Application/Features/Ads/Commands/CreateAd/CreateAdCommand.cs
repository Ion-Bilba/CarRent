using System;
using System.Collections.Generic;
using System.Text;
using CarRent.Domain.Enums;
using MediatR;

namespace CarRent.Application.Features.Ads.Commands.CreateAd
{
    public record CreateAdCommand
     (
         Guid CarId,
        
         string Title,
         string Description,
         decimal Price,
         AdType AdType
     ) : IRequest<Guid>;
}