using System.ComponentModel.DataAnnotations;

namespace CarRent.Web.Models.Cars;

public class CreateCarRequest
{
    [Required(ErrorMessage = "Marca este obligatorie.")]
    public string Brand { get; set; } = string.Empty;

    [Required(ErrorMessage = "Modelul este obligatoriu.")]
    public string Model { get; set; } = string.Empty;

    [Range(1900, 2100, ErrorMessage = "Anul trebuie să fie valid.")]
    public int Year { get; set; } = DateTime.Now.Year;

    [Required(ErrorMessage = "Culoarea este obligatorie.")]
    public string Color { get; set; } = string.Empty;

    [Range(0, 2000000, ErrorMessage = "Rulajul trebuie să fie un număr pozitiv.")]
    public int Mileage { get; set; }

    public FuelType FuelType { get; set; } = FuelType.Petrol;

    [Range(0, 20000, ErrorMessage = "Capacitatea cilindrică trebuie să fie pozitivă.")]
    public int EngineCapacity { get; set; }

    public Transmission Transmission { get; set; } = Transmission.Manual;

    public TractionType TractionType { get; set; } = TractionType.FrontWheel;
}