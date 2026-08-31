using System.ComponentModel.DataAnnotations;

namespace CarRent.Web.Models.Ads;

public class CreateAdRequest
{
    [Required(ErrorMessage = "Selectarea automobilului este obligatorie.")]
    public Guid CarId { get; set; }

    [Required(ErrorMessage = "Titlul este obligatoriu.")]
    [StringLength(100, ErrorMessage = "Titlul nu poate depăși 100 de caractere.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Descrierea este obligatorie.")]
    public string Description { get; set; } = string.Empty;

    [Range(1, 1000000, ErrorMessage = "Prețul trebuie să fie mai mare decât 0.")]
    public decimal Price { get; set; }

    public AdType AdType { get; set; } = AdType.Rent;
}