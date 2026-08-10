using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.Domain.Entities
{
   public class Car
    {
        public Guid Id { get; set; }

        public string Brand { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public int Year { get; set; }

        public string Color { get; set; } = string.Empty;

        public int Mileage { get; set; }

        public enum FuelType
        {
            Petrol,
            Diesel,
            Hybrid,
            Electric
        }

        public int EngineCapacity { get; set; }

        public enum Transmission
        {
            Manual,
            Automatic
        }

        public ICollection<Ad> Ads { get; set; } = new List<Ad>();
    }
}