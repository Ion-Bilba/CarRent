using CarRent.Application.Features.Ads.Queries;
using FluentValidation;

namespace CarRent.Application.Features.Ads.Queries;

public class GetAdsQueryValidator : AbstractValidator<GetAdsQuery>
{
    public GetAdsQueryValidator()
    {
        RuleFor(x => x.TractionType)
            .IsInEnum()
            .When(x => x.TractionType.HasValue)
            .WithMessage("Tipul de tracțiune selectat nu este valid.");

        RuleFor(x => x.AdType)
            .IsInEnum()
            .When(x => x.AdType.HasValue)
            .WithMessage("Tipul anunțului nu este valid.");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Numărul paginii trebuie să fie cel puțin 1.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100)
            .WithMessage("Dimensiunea paginii trebuie să fie între 1 și 100.");

        RuleFor(x => x.MinPrice)
            .GreaterThanOrEqualTo(0)
            .When(x => x.MinPrice.HasValue)
            .WithMessage("Prețul minim nu poate fi negativ.");

        RuleFor(x => x.MaxPrice)
            .GreaterThanOrEqualTo(x => x.MinPrice ?? 0)
            .When(x => x.MaxPrice.HasValue)
            .WithMessage("Prețul maxim trebuie să fie mai mare sau egal cu prețul minim.");
    }
}