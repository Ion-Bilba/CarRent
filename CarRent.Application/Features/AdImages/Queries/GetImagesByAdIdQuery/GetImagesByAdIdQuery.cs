using CarRent.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;

namespace CarRent.Application.Features.AdImages.Queries.GetImagesByAdIdQuery
{
    public record GetImagesByAdIdQuery(Guid AdId) : IRequest<List<AdImageDto>>;
}