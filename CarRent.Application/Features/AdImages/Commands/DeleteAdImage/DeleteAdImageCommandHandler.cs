using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CarRent.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Features.AdImages.Commands.DeleteAdImage
{
    public class DeleteAdImageCommandHandler : IRequestHandler<DeleteAdImageCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorageService _fileStorageService;

        public DeleteAdImageCommandHandler(IApplicationDbContext context, IFileStorageService fileStorageService)
        {
            _context = context;
            _fileStorageService = fileStorageService;
        }

        public async Task Handle(DeleteAdImageCommand request, CancellationToken cancellationToken)
        {
            var adImage = await _context.AdImages
                .FirstOrDefaultAsync(i => i.Id == request.ImageId && i.AdId == request.AdId, cancellationToken);

            if (adImage == null)
            {
                throw new KeyNotFoundException($"Imaginea cu ID-ul {request.ImageId} nu a fost găsită pentru anunțul specificat.");
            }

            _fileStorageService.DeleteFile(adImage.ImageUrl);

            _context.AdImages.Remove(adImage);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}