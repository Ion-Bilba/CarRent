using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace CarRent.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator() 
        {
            RuleFor(c=>c.FirstName).NotEmpty();
            RuleFor(c=>c.LastName).NotEmpty();
            RuleFor(c=>c.Email).NotEmpty().EmailAddress();
            RuleFor(c =>c.Password).NotEmpty().MinimumLength(6);

            RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?[0-9]{8,15}$")
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
            .WithMessage("Formatul numărului de telefon este invalid.");
        }
    }
}
