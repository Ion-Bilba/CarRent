using FluentValidation;

namespace CarRent.Application.Features.Cars.Queries.GetCarsQuery
{
    public class GetCarsQueryValidator : AbstractValidator<GetCarsQuery>
    {
        public GetCarsQueryValidator()
        {
            RuleFor(x => x.MinYear)
                .GreaterThan(1900)
                .When(x => x.MinYear.HasValue)
                .WithMessage("Anul minim este invalid.");

            RuleFor(x => x.MaxYear)
                .LessThanOrEqualTo(DateTime.UtcNow.Year + 1)
                .When(x => x.MaxYear.HasValue)
                .WithMessage("Anul maxim nu poate fi din viitor.");

            RuleFor(x => x.MaxYear)
                .GreaterThanOrEqualTo(x => x.MinYear!.Value)
                .When(x => x.MinYear.HasValue && x.MaxYear.HasValue)
                .WithMessage("Anul maxim trebuie să fie mai mare sau egal cu anul minim.");
        }
    }
}