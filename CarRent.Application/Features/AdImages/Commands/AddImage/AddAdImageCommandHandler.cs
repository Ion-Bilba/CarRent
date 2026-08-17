using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CarRent.Application.Abstractions;
using CarRent.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Features.AdImages.Commands.AddAdImage
{
    public class AddAdImageCommandHandler : IRequestHandler<AddAdImageCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorageService _fileStorageService;

        public AddAdImageCommandHandler(IApplicationDbContext context, IFileStorageService fileStorageService)
        {
            _context = context;
            _fileStorageService = fileStorageService;
        }

        public async Task<Guid> Handle(AddAdImageCommand request, CancellationToken cancellationToken)
        {
            var ad = await _context.Ads
                .FirstOrDefaultAsync(a => a.Id == request.AdId, cancellationToken);

            if (ad == null)
            {
                throw new KeyNotFoundException($"Anunțul cu ID-ul {request.AdId} nu există.");
            }

            var currentImagesCount = await _context.AdImages
                .CountAsync(i => i.AdId == request.AdId, cancellationToken);

            if (currentImagesCount >= 20)
            {
                throw new InvalidOperationException("Un anunț nu poate avea mai mult de 20 de imagini.");
            }

            var imageUrl = await _fileStorageService.SaveFileAsync(request.Request.File, "images", cancellationToken);

            var maxOrder = await _context.AdImages
                .Where(i => i.AdId == request.AdId)
                .MaxAsync(i => (int?)i.DisplayOrder, cancellationToken) ?? 0;

            var displayOrder = request.Request.DisplayOrder > 0
                ? request.Request.DisplayOrder
                : maxOrder + 1;

            var adImage = new AdImage
            {
                Id = Guid.NewGuid(),
                AdId = request.AdId,
                ImageUrl = imageUrl,
                DisplayOrder = displayOrder
            };

            _context.AdImages.Add(adImage);
            await _context.SaveChangesAsync(cancellationToken);

            return adImage.Id;
        }
    }
}