using CarRent.Application.Abstractions;
using CarRent.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CarRent.Application.Features.AdImages.Commands.AddAdImage
{
    public class AddAdImageCommandHandler : IRequestHandler<AddAdImageCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public AddAdImageCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(AddAdImageCommand request, CancellationToken cancellationToken)
        {
           
            var ad = await _context.Ads
                .FirstOrDefaultAsync(a => a.Id == request.AdId, cancellationToken);

            if (ad == null)
            {
                throw new KeyNotFoundException($"Anunțul cu ID-ul {request.AdId} nu există.");
            }

            
            var userExists = await _context.Users
                .AnyAsync(u => u.Id == ad.UserId, cancellationToken);

            if (!userExists)
            {
                throw new KeyNotFoundException($"Utilizatorul asociat acestui anunț (ID: {ad.UserId}) nu există.");
            }

            
            var currentImagesCount = await _context.AdImages
                .CountAsync(i => i.AdId == request.AdId, cancellationToken);

            if (currentImagesCount >= 20)
            {
                throw new InvalidOperationException("Un anunț nu poate avea mai mult de 20 de imagini.");
            }

            var adImage = new AdImage
            {
                Id = Guid.NewGuid(),
                AdId = request.AdId,
                ImageUrl = request.Request.ImageUrl,
                DisplayOrder = request.Request.DisplayOrder
            };

            _context.AdImages.Add(adImage);
            await _context.SaveChangesAsync(cancellationToken);

            return adImage.Id;
        }
    }
}