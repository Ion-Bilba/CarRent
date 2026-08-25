using CarRent.Application.Abstractions;
using CarRent.Application.Common.Interfaces;
using CarRent.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Features.Favorites.Commands.AddFavorite;

public class AddFavoriteCommandHandler : IRequestHandler<AddFavoriteCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public AddFavoriteCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<bool> Handle(AddFavoriteCommand request, CancellationToken cancellationToken)
    {
      
        var currentUserId = _currentUserService.UserId
            ?? throw new UnauthorizedAccessException("Utilizatorul nu este autentificat.");

     
        var adExists = await _context.Ads.AnyAsync(a => a.Id == request.AdId, cancellationToken);
        if (!adExists)
        {
            throw new KeyNotFoundException($"Anunțul cu ID-ul {request.AdId} nu există.");
        }

      
        var alreadyFavorite = await _context.Favorites
            .AnyAsync(f => f.UserId == currentUserId && f.AdId == request.AdId, cancellationToken);

        if (alreadyFavorite)
        {
            return true;
        }

      
        var favorite = new Favorite
        {
            Id = Guid.NewGuid(),
            UserId = currentUserId,
            AdId = request.AdId,
            SavedAt = DateTime.UtcNow
        };

        _context.Favorites.Add(favorite);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}