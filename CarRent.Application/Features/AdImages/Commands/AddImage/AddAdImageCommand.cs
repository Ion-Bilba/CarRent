using MediatR;
using System;

namespace CarRent.Application.Features.AdImages.Commands.AddAdImage
{
    public record AddAdImageRequest(
        string ImageUrl,
        int DisplayOrder
    );

    public record AddAdImageCommand(Guid AdId, AddAdImageRequest Request) : IRequest<Guid>;
}