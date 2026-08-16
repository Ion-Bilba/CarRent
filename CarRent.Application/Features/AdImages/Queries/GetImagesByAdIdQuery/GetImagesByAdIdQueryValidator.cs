using FluentValidation;

namespace CarRent.Application.Features.AdImages.Queries.GetImagesByAdIdQuery
{
    public class GetImagesByAdIdQueryValidator : AbstractValidator<GetImagesByAdIdQuery>
    {
        public GetImagesByAdIdQueryValidator()
        {
            RuleFor(x => x.AdId)
                .NotEmpty().WithMessage("ID-ul anunțului este obligatoriu.");
        }
    }
}