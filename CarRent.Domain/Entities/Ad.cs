using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.Domain.Entities
{
   public class Ad
    {
        public Guid Id { get; set; }

        public Guid CarId { get; set; }

        public Guid UserId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public enum AdType
        {
            Rent,
            Sale
        }

        public bool IsActive { get; set; }

        public int Views { get; set; }

        public DateTime CreatedAt { get; set; } 

        public DateTime? UpdatedAt { get; set; }

        public User User { get; set; } = null!;

        public Car Car { get; set; } = null!;

        public ICollection<AdImage> AdImages { get; set; } = new List<AdImage>();

        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    }
}
