using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.Application.DTOs
{
    public record AdImageDto
    (
        Guid Id,
        Guid AdId,
        string ImageUrl,
        int DisplayOrder
    );
}
