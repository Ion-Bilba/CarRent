using System;
using CarRent.Application.DTOs;
using CarRent.Domain.Enums;
using MediatR;

namespace CarRent.Application.Features.Ads.Queries
{
   public record GetAdsQuery 
    (
    string? SearchTerm = null,
    decimal? MinPrice = null,
    decimal? MaxPrice = null,
    AdType? AdType = null,
    TractionType? TractionType = null,
    int PageNumber = 1,
    int PageSize = 10
    ): IRequest<PagedResult<AdDto>>;
}
