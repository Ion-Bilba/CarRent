using Microsoft.AspNetCore.Mvc;
using MediatR;
using CarRent.Application.Features.Ads.Commands;
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
        var adId= await _mediator.Send(command, cancellationToken);

        return Ok(adId);
    }
}
