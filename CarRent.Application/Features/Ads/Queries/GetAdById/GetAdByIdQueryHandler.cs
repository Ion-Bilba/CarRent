using CarRent.Application.Abstractions;
using CarRent.Application.DTOs;
using CarRent.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.Application.Features.Ads.Queries.GetAdById
{
    public class GetAdByIdQueryHandler : IRequestHandler<GetAdByIdQuery, AdDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetAdByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<AdDto?> Handle(GetAdByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Ads.AsNoTracking()
                 .Where(ad => ad.Id == request.Id)
                 .Select(ad => new AdDto
                 {
                     Id = ad.Id,
                     CarId = ad.CarId,
                     UserId = ad.UserId,
                     Title = ad.Title,
                     Description = ad.Description,
                     Price = ad.Price,
                     AdType = ad.AdType,
                     IsActive = ad.IsActive,
                     Views = ad.Views,
                     CreatedAt = ad.CreatedAt


                 })
                 .FirstOrDefaultAsync(cancellationToken);


        }
    }
}
