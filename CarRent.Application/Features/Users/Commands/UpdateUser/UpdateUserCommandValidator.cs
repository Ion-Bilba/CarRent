using FluentValidation;

namespace CarRent.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID-ul utilizatorului este obligatoriu.");

        RuleFor(x => x.Request.FirstName)
            .NotEmpty().WithMessage("Prenumele este obligatoriu.")
            .MaximumLength(50).WithMessage("Prenumele nu poate depăși 50 de caractere.");

        RuleFor(x => x.Request.LastName)
            .NotEmpty().WithMessage("Numele este obligatoriu.")
            .MaximumLength(50).WithMessage("Numele nu poate depăși 50 de caractere.");

        RuleFor(x => x.Request.Email)
            .NotEmpty().WithMessage("Email-ul este obligatoriu.")
            .EmailAddress().WithMessage("Formatul adresei de email este invalid.")
            .MaximumLength(100).WithMessage("Email-ul nu poate depăși 100 de caractere.");

        RuleFor(x => x.Request.PhoneNumber)
            .MaximumLength(20).WithMessage("Numărul de telefon este prea lung.");
    }
}