using CarRent.Application.Abstractions;
using CarRent.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CarRent.Application.Features.Favorites.Commands.DeleteFavorite
{
    public class DeleteFavoriteCommandHandler : IRequestHandler<DeleteFavoriteCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public DeleteFavoriteCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(DeleteFavoriteCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException("Utilizatorul nu este autentificat.");

            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == currentUserId && f.AdId == request.AdId, cancellationToken);

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