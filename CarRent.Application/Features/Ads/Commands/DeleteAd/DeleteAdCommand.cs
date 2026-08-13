using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.Application.Features.Ads.Commands.DeleteAd
{
    public record DeleteAdCommand(Guid Id) : IRequest<bool>;

}
