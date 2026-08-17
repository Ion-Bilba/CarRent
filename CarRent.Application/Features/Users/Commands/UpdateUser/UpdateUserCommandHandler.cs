using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CarRent.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

        if (user == null)
        {
            throw new KeyNotFoundException($"Utilizatorul cu ID-ul {request.Id} nu a fost găsit.");
        }

        user.FirstName = request.Request.FirstName;
        user.LastName = request.Request.LastName;
        user.Email = request.Request.Email;
        user.PhoneNumber = request.Request.PhoneNumber;

        await _context.SaveChangesAsync(cancellationToken);
    }
}