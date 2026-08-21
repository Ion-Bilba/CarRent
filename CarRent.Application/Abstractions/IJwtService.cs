using System;
using System.Collections.Generic;
using System.Text;
using CarRent.Domain.Entities;

namespace CarRent.Application.Abstractions
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
