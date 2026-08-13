using CarRent.Application.DTOs;
using MediatR;

namespace CarRent.Application.Features.Ads.Queries.GetAdById;

public record GetAdByIdQuery(Guid Id) : IRequest<AdDto?>;