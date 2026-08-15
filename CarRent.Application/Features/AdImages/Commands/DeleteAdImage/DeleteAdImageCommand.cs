using MediatR;
using System;

namespace CarRent.Application.Features.AdImages.Commands.DeleteAdImage
{
    public record DeleteAdImageCommand(Guid AdId, Guid ImageId) : IRequest<bool>;
}