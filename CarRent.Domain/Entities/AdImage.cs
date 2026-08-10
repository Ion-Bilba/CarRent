using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.Domain.Entities
{
    public class AdImage
    {
        public Guid Id { get; set; }

        public Guid AdId { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }

        public Ad Ad { get; set; } = null!;
    }
}