using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using CarRent.Application.Abstractions;
using BCrypt.Net;

namespace CarRent.Infrastructure.Authentication
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password,passwordHash);
        }
    }
}
