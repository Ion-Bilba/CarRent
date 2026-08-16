using System.Threading;
using System.Threading.Tasks;
using CarRent.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Features.Favorites.Queries.IsFavorite
{
    public class IsFavoriteQueryHandler : IRequestHandler<IsFavoriteQuery, bool>
    {
        private readonly IApplicationDbContext _context;

        public IsFavoriteQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(IsFavoriteQuery request, CancellationToken cancellationToken)
        {
            return await _context.Favorites
                .AsNoTracking()
                .AnyAsync(f => f.UserId == request.UserId && f.AdId == request.AdId, cancellationToken);
        }
    }
}