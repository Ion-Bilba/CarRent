using CarRent.Application.DTOs;
using CarRent.Application.Features.AdImages.Commands.AddAdImage;
using CarRent.Application.Features.AdImages.Commands.DeleteAdImage;
using CarRent.Application.Features.AdImages.Commands.UpdateAdImage;
using CarRent.Application.Features.AdImages.Queries;
using CarRent.Application.Features.AdImages.Queries.GetImagesByAdIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarRent.Api.Controllers
{
    [ApiController]
    [Route("api/ads/{adId:guid}/images")]
    public class AdImagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdImagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetByAd([FromRoute] Guid adId, CancellationToken cancellationToken)
        {
            var images = await _mediator.Send(new GetImagesByAdIdQuery(adId), cancellationToken);
            return Ok(images);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromRoute] Guid adId, [FromBody] AddAdImageRequest request, CancellationToken cancellationToken)
        {
            var imageId = await _mediator.Send(new AddAdImageCommand(adId, request), cancellationToken);
            return Ok(imageId);
        }

        [HttpPut("{imageId:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid adId, [FromRoute] Guid imageId, [FromBody] UpdateAdImageRequest request, CancellationToken cancellationToken)
        {
            var updated = await _mediator.Send(new UpdateAdImageCommand(adId, imageId, request), cancellationToken);
            if (!updated) return NotFound();

            return NoContent();
        }

        [HttpDelete("{imageId:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid adId, [FromRoute] Guid imageId, CancellationToken cancellationToken)
        {
            var deleted = await _mediator.Send(new DeleteAdImageCommand(adId, imageId), cancellationToken);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}