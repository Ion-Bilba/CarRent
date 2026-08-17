using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CarRent.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Features.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

        if (user == null)
        {
            throw new KeyNotFoundException($"Utilizatorul cu ID-ul {request.Id} nu a fost găsit.");
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
    }
}