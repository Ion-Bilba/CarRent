using CarRent.Domain.Enums;

namespace CarRent.Application.Features.Ads.Commands.UpdateAd
{
    public record UpdateAdRequest(
        string Title,
        string Description,
        decimal Price,
        AdType AdType
    );
}