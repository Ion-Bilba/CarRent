using CarRent.Application.Abstractions;
using CarRent.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CarRent.Application.Features.AdImages.Commands.UpdateAdImage
{
    public class UpdateAdImageCommandHandler : IRequestHandler<UpdateAdImageCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public UpdateAdImageCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(UpdateAdImageCommand request, CancellationToken cancellationToken)
        {
           
            var currentUserId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException("Utilizatorul nu este autentificat.");

          
            var image = await _context.AdImages
                .Include(i => i.Ad)
                .FirstOrDefaultAsync(i => i.Id == request.ImageId && i.AdId == request.AdId, cancellationToken);

            if (image == null)
            {
                throw new KeyNotFoundException($"Imaginea cu ID-ul {request.ImageId} nu a fost găsită pentru anunțul specificat.");
            }

           
            if (image.Ad.UserId != currentUserId && !_currentUserService.IsAdmin)
            {
                throw new UnauthorizedAccessException("Nu poți modifica o imagine dintr-un anunț care nu îți aparține.");
            }

            image.ImageUrl = request.Request.ImageUrl;
            image.DisplayOrder = request.Request.DisplayOrder;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}