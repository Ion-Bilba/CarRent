using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.Application.Features.Ads.Commands.CreateAd
{
    public class CreateAdCommandValidator  : AbstractValidator<CreateAdCommand>
    {
        public CreateAdCommandValidator() 
        {
            RuleFor(c => c.Title).NotEmpty()
                .WithMessage("Titlul este obligatoriu.")
                .MaximumLength(100).WithMessage("Titlul nu poate depăși 100 de caractere.");

            RuleFor(c => c.Description).NotEmpty()
                .WithMessage("Descrierea este obligatorie.")
                .MaximumLength(2000).WithMessage("Descrierea nu poate depăși 2000 de caractere.");

            RuleFor(c => c.Price).GreaterThan(0);

            RuleFor(c => c.AdType).IsInEnum().WithMessage("Tipul anunțului este invalid");

            RuleFor(c => c.CarId).NotEmpty().WithMessage("ID-ul mașinii este obligatoriu.");

           
        }
    }
}