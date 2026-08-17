using System;
using CarRent.Application.Features.Users.DTOs;
using MediatR;

namespace CarRent.Application.Features.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid Id) : IRequest<UserDto>;