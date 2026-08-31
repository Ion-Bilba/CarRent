namespace CarRent.Web.Models.Cars;

public class CarDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Color { get; set; } = string.Empty;
    public int Mileage { get; set; }
    public FuelType FuelType { get; set; }
    public int EngineCapacity { get; set; }
    public Transmission Transmission { get; set; }
    public TractionType TractionType { get; set; }
}