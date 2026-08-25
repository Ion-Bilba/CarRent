using CarRent.Application.Abstractions;
using CarRent.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarRent.Application.Features.Favorites.Queries.IsFavorite
{
    public class IsFavoriteQueryHandler : IRequestHandler<IsFavoriteQuery, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public IsFavoriteQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(IsFavoriteQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException("Utilizatorul nu este autentificat.");

            return await _context.Favorites
                .AsNoTracking()
                .AnyAsync(f => f.UserId == currentUserId && f.AdId == request.AdId, cancellationToken);
        }
    }
}