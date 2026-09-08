using System;
using CarRent.Domain.Enums;


namespace CarRent.Application.DTOs
{
    public class AdDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CarId { get; set; }
        public string Title {  get; set; }=string.Empty;
        public string Description {  get; set; }=string.Empty;
        public decimal Price { get; set; }
        public AdType AdType { get; set; }
        public TractionType TractionType { get; set; }
        public bool IsActive { get; set; }
        public int Views {  get; set; }
        public DateTime CreatedAt { get; set; }
        public string? MainImageUrl { get; set; }

        public string CarBrand { get; set; } = string.Empty;
        public string CarModel { get; set; } = string.Empty;
        public int CarYear { get; set; }
        public string CarColor { get; set; } = string.Empty;
        public int CarMileage { get; set; }
        public FuelType CarFuelType { get; set; }
        public int CarEngineCapacity { get; set; }
        public Transmission CarTransmission { get; set; }

        public List<AdImageDto>AdImages {get; set;}= new List<AdImageDto>();
    }
    
}