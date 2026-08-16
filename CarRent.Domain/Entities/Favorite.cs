using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.Domain.Entities
{
    public class Favorite
    {
        public Guid Id { get; set; }    

        public Guid UserId { get; set; }    

        public Guid AdId { get; set; }  

        public DateTime SavedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; } = null!;

        public Ad Ad { get; set; } = null!;
    }
}