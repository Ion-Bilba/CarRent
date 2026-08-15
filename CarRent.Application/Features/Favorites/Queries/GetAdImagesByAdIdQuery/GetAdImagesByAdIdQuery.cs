using CarRent.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;

namespace CarRent.Application.Features.AdImages.Queries.GetAdImagesByAdId
{
    public record GetAdImagesByAdIdQuery(Guid AdId) : IRequest<List<AdImageDto>>;
}