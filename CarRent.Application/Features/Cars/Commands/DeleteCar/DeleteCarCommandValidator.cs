using FluentValidation;

namespace CarRent.Application.Features.Cars.Commands.DeleteCar
{
    public class DeleteCarCommandValidator : AbstractValidator<DeleteCarCommand>
    {
        public DeleteCarCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID-ul este obligatoriu pentru ștergere.");
        }
    }
}