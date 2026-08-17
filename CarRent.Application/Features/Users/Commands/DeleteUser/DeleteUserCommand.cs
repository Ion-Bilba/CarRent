using System;
using MediatR;

namespace CarRent.Application.Features.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid Id) : IRequest;