using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CarRent.Application.Abstractions;
using CarRent.Application.Features.Users.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Features.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllUsersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .Select(u => new UserDto(
                u.Id,
                u.FirstName,
                u.LastName,
                u.Email,
                u.PhoneNumber?? string.Empty
            ))
            .ToListAsync(cancellationToken);
    }
}