using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CarRent.Application.DTOs;
using CarRent.Application.Features.Favorites.Commands.AddFavorite;
using CarRent.Application.Features.Favorites.Commands.DeleteFavorite;
using CarRent.Application.Features.Favorites.Queries.GetMyFavorites;
using CarRent.Application.Features.Favorites.Queries.IsFavorite;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/favorites")]
    public class FavoritesController : ControllerBase
    {
        private readonly ISender _mediator;

        public FavoritesController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<AdDto>>> GetMyFavorites(CancellationToken cancellationToken)
        {
           
            var result = await _mediator.Send(new GetMyFavoritesQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{adId:guid}/status")]
        public async Task<ActionResult<bool>> IsFavorite(Guid adId, CancellationToken cancellationToken)
        {
           
            var result = await _mediator.Send(new IsFavoriteQuery(adId), cancellationToken);
            return Ok(result);
        }

        [HttpPost("{adId:guid}")]
        public async Task<IActionResult> Add(Guid adId, CancellationToken cancellationToken)
        {
            await _mediator.Send(new AddFavoriteCommand(adId), cancellationToken);
            return Ok();
        }

        [HttpDelete("{adId:guid}")]
        public async Task<IActionResult> Delete(Guid adId, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteFavoriteCommand(adId), cancellationToken);
            return NoContent();
        }
    }
}