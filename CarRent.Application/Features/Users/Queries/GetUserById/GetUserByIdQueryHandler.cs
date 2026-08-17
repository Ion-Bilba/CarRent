using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CarRent.Application.Abstractions;
using CarRent.Application.Features.Users.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IApplicationDbContext _context;

    public GetUserByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

        if (user == null)
        {
            throw new KeyNotFoundException($"Utilizatorul cu ID-ul {request.Id} nu a fost găsit.");
        }

        return new UserDto(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            user.PhoneNumber
        );
    }
}