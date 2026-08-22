using CarRent.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }=string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email {  get; set; } = string.Empty;

        public string PasswordHash {  get; set; } = string.Empty;

        public string? PhoneNumber {  get; set; }

        public UserRole UserRole { get; set; }= UserRole.User;

        public DateTime CreatedAt { get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();

        public ICollection<Ad> Ads { get; set; } = new List<Ad>();

        public ICollection<Favorite>Favorites { get; set; }= new List<Favorite>();
    }
}