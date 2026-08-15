using CarRent.Application.DTOs;
using CarRent.Application.Features.Ads.Commands.CreateAd;
using CarRent.Application.Features.Ads.Commands.DeleteAd;
using CarRent.Application.Features.Ads.Commands.UpdateAd;
using CarRent.Application.Features.Ads.Commands.UpdateAdCommand;
using CarRent.Application.Features.Ads.Queries.GetAdById;
using CarRent.Application.Features.Ads.Queries.GetAdsQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace CarRent.API.Controllers;


[ApiController]
[Route("api/[Controller]")]
public class AdsController : ControllerBase
{

    private readonly ISender _mediator;

    public AdsController(ISender mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAdCommand command, CancellationToken cancellationToken)
    {
        var adId = await _mediator.Send(command, cancellationToken);

        return Ok(adId);
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<AdDto>>> GetAll([FromQuery] GetAdsQuery query,CancellationToken cancellationToken)
    {
        var ads = await _mediator.Send( query, cancellationToken);
        return Ok(ads);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAdRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateAdCommand(
            id,
            request.Title,
            request.Description,
            request.Price,
            request.AdType
            
        );

        var isUpdated = await _mediator.Send(command, cancellationToken);

        if (!isUpdated)
        {
            return NotFound($"Anunțul cu ID-ul {id} nu a fost găsit.");
        }

        return NoContent();
    }

    [HttpDelete("{Id:guid}")]
    public async Task<IActionResult> Delete(Guid Id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteAdCommand(Id), cancellationToken);

        if (!result)
        {
            return NotFound($"Anunțul cu ID-ul {Id} nu a fost găsit.");
        }

        return NoContent();
    }
    [HttpGet("{Id:guid}")]
    public async Task<IActionResult>GetById (Guid Id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAdByIdQuery(Id), cancellationToken);

        if (result==null)
        {
            return NotFound($"Anunțul cu ID-ul {Id} nu a fost găsit.");
        }
        return Ok(result);
    }

} 
