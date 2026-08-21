using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace CarRent.Application.Features.Auth.Login
{
   public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator() 
        {
            RuleFor(c => c.Password).NotEmpty().WithMessage("Parola este obligatorie.");
            RuleFor(c=>c.Email).NotEmpty().WithMessage("Email-ul este obligatoriu!").EmailAddress().WithMessage("Format de emali invalid!");
        }
    }
}
