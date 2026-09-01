using System.ComponentModel.DataAnnotations;

namespace CarRent.Web.Models.Users;

public class UpdateUserRequest
{
    [Required(ErrorMessage = "Prenumele este obligatoriu.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Numele este obligatoriu.")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email-ul este obligatoriu.")]
    [EmailAddress(ErrorMessage = "Adresa de email nu este validă.")]
    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
}