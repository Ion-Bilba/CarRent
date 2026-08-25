using CarRent.Application.Abstractions;
using CarRent.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CarRent.Application.Features.Cars.Commands.DeleteCar
{
    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public DeleteCarCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            
            var currentUserId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException("Utilizatorul nu este autentificat.");

            
            var car = await _context.Cars
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (car == null)
            {
                throw new KeyNotFoundException($"Mașina cu ID-ul {request.Id} nu a fost găsită.");
            }

            
            if (car.UserId != currentUserId && !_currentUserService.IsAdmin)
            {
                throw new UnauthorizedAccessException("Nu poți șterge o mașină care nu îți aparține.");
            }

           
            var hasAds = await _context.Ads.AnyAsync(a => a.CarId == request.Id, cancellationToken);
            if (hasAds)
            {
                throw new InvalidOperationException("Nu poți șterge o mașină care are anunțuri legate de ea.");
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}