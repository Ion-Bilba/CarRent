using CarRent.Application.Abstractions;
using CarRent.Domain.Entities;
using CarRent.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Data;
namespace CarRent.Application.Features.Auth.Commands.Register
{
   public class RegisterCommandHandler : IRequestHandler<RegisterCommand,Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterCommandHandler (IApplicationDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var emailExits= await _context.Users
                .AnyAsync(c=>c.Email == request.Email,cancellationToken);

            if(emailExits)
            {
                throw new BadHttpRequestException("Email-ul este deja utilizat.");
            }

            var passwordHash = _passwordHasher.Hash(request.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                PasswordHash = passwordHash,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                UserRole=UserRole.User

            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
            return user.Id;

        }
    }
}
