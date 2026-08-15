using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.Application.Features.Cars.Commands.CreatedCar
{
    public class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
    {
        public CreateCarCommandValidator()
        {
            RuleFor(c => c.Model).NotEmpty().WithMessage("Modelul trebuie specificat").MaximumLength(50).WithMessage("Modelul nu poate depăși 50 de caractere.");

            RuleFor(c => c.Brand).NotEmpty().WithMessage("Brandul trebuie specificat").MaximumLength(50).WithMessage("Brandul nu poate depăși 50 de caractere.");

            RuleFor(c => c.Year).NotEmpty().WithMessage("Anul trebuie specificat").InclusiveBetween(1886, DateTime.UtcNow.Year + 1);

            RuleFor(c => c.Color).NotEmpty().WithMessage("Culoarea trebuie specificat").MaximumLength(20).WithMessage("Culoarea nu poate depăși 20 de caractere.");

            RuleFor(C => C.Mileage).GreaterThanOrEqualTo(0).WithMessage("Kilometrajul nu poate fi negativ.");

            RuleFor(c => c.FuelType).IsInEnum().WithMessage("Tipul de combustibil nu este valid");

            RuleFor(c => c.EngineCapacity).GreaterThanOrEqualTo(0).WithMessage("Capacitatea motorului trebuie să fie mai mare decât 0.");

            RuleFor(c => c.Transmission).IsInEnum().WithMessage("Tipul de Transmisie nu este valid");

            RuleFor(c => c.TractionType).IsInEnum().WithMessage("Tipul de Tractiune nu este valid");


        }

    }
}