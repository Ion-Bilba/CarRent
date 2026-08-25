using CarRent.Application.Abstractions;
using CarRent.Application.Common.Interfaces;
using CarRent.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CarRent.Application.Features.Ads.Commands.UpdateAdCommand
{
    public class UpdateAdCommandHandler : IRequestHandler<UpdateAdCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public UpdateAdCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(UpdateAdCommand request, CancellationToken cancellationToken)
        {
           
            var currentUserId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException("Utilizatorul nu este autentificat.");

           
            var ad = await _context.Ads
               .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (ad == null)
            {
                throw new KeyNotFoundException($"Anunțul cu ID-ul {request.Id} nu a fost găsit.");
            }

           
            if (ad.UserId != currentUserId && !_currentUserService.IsAdmin)
            {
                throw new UnauthorizedAccessException("Nu poți modifica un anunț care nu îți aparține.");
            }

            ad.Title = request.Title;
            ad.Description = request.Description;
            ad.Price = request.Price;
            ad.AdType = request.AdType;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}