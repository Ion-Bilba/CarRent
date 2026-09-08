using System;
using CarRent.Application.DTOs;
using CarRent.Domain.Enums;
using MediatR;

namespace CarRent.Application.Features.Ads.Queries.GetAdsQuery
{
   public record GetAdsQuery 
    (
    string? SearchTerm = null,
    decimal? MinPrice = null,
    decimal? MaxPrice = null,
    AdType? AdType = null,
    TractionType? TractionType = null,
    string? Brand = null,
    string? Model = null,
    int? MinYear = null,
    int? MaxYear = null,
    FuelType? FuelType = null,
    Transmission? Transmission = null,
    int PageNumber = 1,
    int PageSize = 10
    ): IRequest<PagedResult<AdDto>>;
}
