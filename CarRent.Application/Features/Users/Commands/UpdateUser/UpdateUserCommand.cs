using System;
using MediatR;

namespace CarRent.Application.Features.Users.Commands.UpdateUser;

public record UpdateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber

);

public record UpdateUserCommand(Guid Id, UpdateUserRequest Request) : IRequest;