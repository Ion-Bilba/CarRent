using CarRent.Domain.Enums;


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

        public FuelType FuelType { get; set; }

        public int EngineCapacity { get; set; }

        public Transmission Transmission { get; set; }

        public TractionType TractionType { get; set; }
        public ICollection<Ad> Ads { get; set; } = new List<Ad>();
    }
}