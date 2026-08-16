using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CarRent.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Features.Favorites.Commands.DeleteFavorite
{
    public class DeleteFavoriteCommandHandler : IRequestHandler<DeleteFavoriteCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteFavoriteCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteFavoriteCommand request, CancellationToken cancellationToken)
        {
            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == request.UserId && f.AdId == request.AdId, cancellationToken);

            if (favorite == null)
            {
                throw new KeyNotFoundException("Anunțul nu se află în lista de favorite.");
            }

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}