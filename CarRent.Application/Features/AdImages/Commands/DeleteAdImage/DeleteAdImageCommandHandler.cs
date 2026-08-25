using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CarRent.Application.Abstractions;
using CarRent.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Features.AdImages.Commands.DeleteAdImage
{
    public class DeleteAdImageCommandHandler : IRequestHandler<DeleteAdImageCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorageService _fileStorageService;
        private readonly ICurrentUserService _currentUserService;

        public DeleteAdImageCommandHandler(
            IApplicationDbContext context,
            IFileStorageService _fileStorageService,
            ICurrentUserService currentUserService)
        {
            _context = context;
            this._fileStorageService = _fileStorageService;
            _currentUserService = currentUserService;
        }

        public async Task Handle(DeleteAdImageCommand request, CancellationToken cancellationToken)
        {
           
            var currentUserId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException("Utilizatorul nu este autentificat.");

           
            var adImage = await _context.AdImages
                .Include(i => i.Ad)
                .FirstOrDefaultAsync(i => i.Id == request.ImageId && i.AdId == request.AdId, cancellationToken);

            if (adImage == null)
            {
                throw new KeyNotFoundException($"Imaginea cu ID-ul {request.ImageId} nu a fost găsită pentru anunțul specificat.");
            }

            
            if (adImage.Ad.UserId != currentUserId && !_currentUserService.IsAdmin)
            {
                throw new UnauthorizedAccessException("Nu poți șterge o imagine dintr-un anunț care nu îți aparține.");
            }

            _fileStorageService.DeleteFile(adImage.ImageUrl);

            _context.AdImages.Remove(adImage);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}