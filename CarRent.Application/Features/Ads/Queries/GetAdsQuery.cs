using System;
using CarRent.Application.DTOs;
using MediatR;

namespace CarRent.Application.Features.Ads.Queries
{
   public class GetAdsQuery : IRequest<List<AdDto>>
    {

    }
}
