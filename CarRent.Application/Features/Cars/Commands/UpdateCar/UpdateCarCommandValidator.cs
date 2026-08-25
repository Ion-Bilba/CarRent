using FluentValidation;
using System;

namespace CarRent.Application.Features.Cars.Commands.UpdateCar
{
    public class UpdateCarCommandValidator : AbstractValidator<UpdateCarCommand>
    {
        public UpdateCarCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID-ul mașinii este obligatoriu.");

            RuleFor(x => x.Brand)
                .NotEmpty().WithMessage("Marca este obligatorie.")
                .MaximumLength(50).WithMessage("Marca nu poate depăși 50 de caractere.");

            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("Modelul este obligatoriu.")
                .MaximumLength(50).WithMessage("Modelul nu poate depăși 50 de caractere.");

            RuleFor(x => x.Year)
                .InclusiveBetween(1886, DateTime.UtcNow.Year + 1)
                .WithMessage("Anul de fabricație este invalid.");

            RuleFor(x => x.Mileage)
                .GreaterThanOrEqualTo(0).WithMessage("Kilometrajul nu poate fi negativ.");

            RuleFor(x => x.EngineCapacity)
                .GreaterThanOrEqualTo(0).WithMessage("Capacitatea motorului nu poate fi negativă.");

            RuleFor(c => c.Transmission)
                .IsInEnum().WithMessage("Tipul de transmisie este invalid.");

            RuleFor(c => c.FuelType)
                .IsInEnum().WithMessage("Tipul de combustibil este invalid.");

            RuleFor(c => c.TractionType)
                .IsInEnum().WithMessage("Tipul de tracțiune este invalid.");
        }
    }
}