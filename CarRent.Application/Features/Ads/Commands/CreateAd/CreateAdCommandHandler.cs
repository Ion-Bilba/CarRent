using CarRent.Application.Abstractions;
using CarRent.Application.Common.Interfaces;
using CarRent.Domain.Entities;
using MediatR;

namespace CarRent.Application.Features.Ads.Commands.CreateAd;

public class CreateAdCommandHandler : IRequestHandler<CreateAdCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateAdCommandHandler(IApplicationDbContext context,ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreateAdCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId
            ?? throw new UnauthorizedAccessException("Utilizatorul nu este autentificat.");

        var car = await _context.Cars
            .FindAsync(new object[] { request.CarId }, cancellationToken);

        if (car == null)
        {
            throw new KeyNotFoundException($"Mașina cu ID-ul {request.CarId} nu există.");
        }

        if (car.UserId != currentUserId)
        {
            throw new UnauthorizedAccessException("Nu poți crea un anunț cu o mașină care nu îți aparține.");
        }


        var ad = new Ad
        {
            Id = Guid.NewGuid(),
            CarId = request.CarId,
            UserId = currentUserId,
            Title = request.Title,
            Description = request.Description,
            Price = request.Price,
            AdType = request.AdType
        };

        _context.Ads.Add(ad);
        await _context.SaveChangesAsync(cancellationToken);

        return ad.Id;
    }
}