using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
namespace CarRent.Application.Features.Ads.Commands.UpdateAdCommand
{
   public class UpdateAdCommandValidator : AbstractValidator<UpdateAdCommand>
    {
        public UpdateAdCommandValidator()
        {
            RuleFor(c=>c.Title).NotEmpty().WithMessage("Titlul este obligator. ").MaximumLength(100).WithMessage("Titlul nu poate depăși 100 de caractere.");

            RuleFor(C => C.Description).NotEmpty().WithMessage("Descrierea este obligatorie").MaximumLength(2000).WithMessage("Descrierea nu poate depăși 2000 de caractere");

            RuleFor(c => c.Price).GreaterThan(0);

            RuleFor(c => c.AdType).IsInEnum().WithMessage("Tipul anunțului este invalid");

            RuleFor(c => c.Id).NotEmpty().WithMessage("ID-ul este obligatoriu.");

        }
    }
}