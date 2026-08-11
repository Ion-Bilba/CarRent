using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace CarRent.Application.Features.Ads.Commands
{
    public record CreateAdCommand
     (
         Guid CarId,
         Guid UserId,
         string Title,
         string Description,
         decimal Price,
         string AdType
     ) : IRequest<Guid>;
}