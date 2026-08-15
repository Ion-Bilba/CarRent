using MediatR;
using System;

namespace CarRent.Application.Features.AdImages.Commands.UpdateAdImage
{
    public record UpdateAdImageRequest(
        string ImageUrl,
        int DisplayOrder
    );

    public record UpdateAdImageCommand(
        Guid AdId,
        Guid ImageId,
        UpdateAdImageRequest Request
    ) : IRequest<bool>;
}