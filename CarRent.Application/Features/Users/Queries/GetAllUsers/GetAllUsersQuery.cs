using System.Collections.Generic;
using CarRent.Application.Features.Users.DTOs;
using MediatR;

namespace CarRent.Application.Features.Users.Queries.GetAllUsers;

public record GetAllUsersQuery() : IRequest<List<UserDto>>;