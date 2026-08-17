using FluentValidation;

namespace CarRent.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID-ul utilizatorului este obligatoriu și nu poate fi un GUID gol.");
    }
}