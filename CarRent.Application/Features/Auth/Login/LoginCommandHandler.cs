using CarRent.Application.Abstractions;
using CarRent.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
namespace CarRent.Application.Features.Auth.Login
{
    public class LoginCommandHandler: IRequestHandler<LoginCommand,AuthResponseDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;
        
        public LoginCommandHandler(IJwtService jwtService, IApplicationDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _jwtService=jwtService;
            _passwordHasher=passwordHasher;
        }

        public async Task<AuthResponseDto>Handle(LoginCommand request,CancellationToken cancellationToken)
        {
            var user= await _context.Users
                .FirstOrDefaultAsync(c=>c.Email == request.Email,cancellationToken);
            if(user==null)
            {
                throw new BadHttpRequestException("Email sau parolă incorectă.");
            }

            var isPasswordValid= _passwordHasher.Verify(request.Password,user.PasswordHash);

            if (isPasswordValid==false)
            {
                throw new BadHttpRequestException("Email sau parolă incorectă.");
            }

            var token = _jwtService.GenerateToken(user);

            return new AuthResponseDto(user.Id, user.Email,token);
        }

    }
}
