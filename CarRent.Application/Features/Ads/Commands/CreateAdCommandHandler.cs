using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using CarRent.Domain.Entities;
using CarRent.Domain.Enums;
using CarRent.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Features.Ads.Commands
{
    public class CreateAdCommandHandler : IRequestHandler<CreateAdCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateAdCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateAdCommand request, CancellationToken cancellationToken)
        {
            var carExists = await _context.Cars
                .AnyAsync(c => c.Id == request.CarId, cancellationToken);

            if (!carExists)
            {
                throw new KeyNotFoundException($"Mașina cu ID-ul {request.CarId} nu există.");
            }

            // 2. Verificăm dacă utilizatorul există în baza de date
            var userExists = await _context.Users
                .AnyAsync(u => u.Id == request.UserId, cancellationToken);

            if (!userExists)
            {
                throw new KeyNotFoundException($"Utilizatorul cu ID-ul {request.UserId} nu există.");
            }
            var ad = new Ad
            {
                Id = Guid.NewGuid(),
                CarId = request.CarId,
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                AdType = request.AdType
            };
            _context.Ads.Add(ad);
            await _context.SaveChangesAsync(cancellationToken);
            return ad.Id;

        }
    }
}