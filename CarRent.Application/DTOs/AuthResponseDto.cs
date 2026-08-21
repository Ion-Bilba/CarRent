using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.Application.DTOs
{
    public record AuthResponseDto
    (
       Guid UserId,
       string Email, 
       string token
    );
}
