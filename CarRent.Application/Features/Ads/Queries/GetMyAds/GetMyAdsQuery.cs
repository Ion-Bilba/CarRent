using CarRent.Application.DTOs;
using MediatR;

namespace CarRent.Application.Features.Ads.Queries.GetMyAds
{
    public record GetMyAdsQuery : IRequest<List<AdDto>>;
}