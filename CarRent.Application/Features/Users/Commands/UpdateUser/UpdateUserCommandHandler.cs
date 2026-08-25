using CarRent.Application.Abstractions;
using CarRent.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CarRent.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateUserCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId
            ?? throw new UnauthorizedAccessException("Utilizatorul nu este autentificat.");

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

        if (user == null)
        {
            throw new KeyNotFoundException($"Utilizatorul cu ID-ul {request.Id} nu a fost găsit.");
        }

        if (user.Id != currentUserId && !_currentUserService.IsAdmin)
        {
            throw new UnauthorizedAccessException("Nu poți modifica profilul altui utilizator.");
        }

        user.FirstName = request.Request.FirstName;
        user.LastName = request.Request.LastName;
        user.Email = request.Request.Email;
        user.PhoneNumber = request.Request.PhoneNumber;

        await _context.SaveChangesAsync(cancellationToken);
    }
}