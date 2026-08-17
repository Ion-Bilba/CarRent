using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace CarRent.Application.Features.AdImages.Commands.AddAdImage
{
    public record AddAdImageRequest(
        IFormFile File,
        int DisplayOrder
    );

    public record AddAdImageCommand(Guid AdId, AddAdImageRequest Request) : IRequest<Guid>;
}