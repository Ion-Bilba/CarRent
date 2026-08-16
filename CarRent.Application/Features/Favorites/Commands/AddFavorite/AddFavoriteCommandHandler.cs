using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CarRent.Application.Abstractions;
using CarRent.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Features.Favorites.Commands.AddFavorite
{
    public class AddFavoriteCommandHandler : IRequestHandler<AddFavoriteCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public AddFavoriteCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddFavoriteCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == request.UserId, cancellationToken);
            if (!userExists)
            {
                throw new KeyNotFoundException($"Utilizatorul cu ID-ul {request.UserId} nu există.");
            }

            var adExists = await _context.Ads.AnyAsync(a => a.Id == request.AdId, cancellationToken);
            if (!adExists)
            {
                throw new KeyNotFoundException($"Anunțul cu ID-ul {request.AdId} nu există.");
            }

            var alreadyFavorite = await _context.Favorites
                .AnyAsync(f => f.UserId == request.UserId && f.AdId == request.AdId, cancellationToken);

            if (alreadyFavorite)
            {
                return true;
            }

            var favorite = new Favorite
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                AdId = request.AdId,
                SavedAt = DateTime.UtcNow
            };

            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}