using System;
using System.Threading;
using System.Threading.Tasks;
using CarRent.Application.Features.AdImages.Commands.AddAdImage;
using CarRent.Application.Features.AdImages.Commands.DeleteAdImage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/ads/{adId:guid}/images")]
    public class AdImagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdImagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddImage(
            Guid adId,
            [FromForm] AddAdImageRequest request,
            CancellationToken cancellationToken)
        {
            var command = new AddAdImageCommand(adId, request);
            var imageId = await _mediator.Send(command, cancellationToken);

            return Ok(new { Id = imageId });
        }

        [HttpDelete("{imageId:guid}")]
        public async Task<IActionResult> DeleteImage(
            Guid adId,
            Guid imageId,
            CancellationToken cancellationToken)
        {
            var command = new DeleteAdImageCommand(adId, imageId);
            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}