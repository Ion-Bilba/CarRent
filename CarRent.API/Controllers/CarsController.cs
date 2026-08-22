using CarRent.Application.Features.Ads.Commands.UpdateAdCommand;
using CarRent.Application.Features.Ads.Queries;
using CarRent.Application.Features.Cars.Commands.CreatedCar;
using CarRent.Application.Features.Cars.Commands.DeleteCar;
using CarRent.Application.Features.Cars.Commands.UpdateCar;
using CarRent.Application.Features.Cars.Queries.GetCarById;
using CarRent.Application.Features.Cars.Queries.GetCarsQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CarRent.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ISender _mediator;

        public CarsController(ISender mediator)
        {
            _mediator = mediator;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCarCommand command, CancellationToken cancellationToken)
        {
            var carId = await _mediator.Send(command, cancellationToken);
            return Ok(carId);
        }
        [Authorize]
        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> Delete(Guid Id,CancellationToken cancellationToken)
        {
            var carId = await _mediator.Send(new DeleteCarCommand(Id), cancellationToken);
            if(!carId)
            {
                return NotFound($"Masina cu ID-ul {Id} nu a fost găsit.");
            }
            return NoContent();
        }
        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCarRequest request, CancellationToken cancellationToken)
        {
        
            var command = new UpdateCarCommand(
                id,
                request.Brand,
                request.Model,
                request.Year,
                request.Color,
                request.Mileage,
                request.FuelType,
                request.EngineCapacity,
                request.Transmission,
                request.TractionType
            );

            var isUpdated = await _mediator.Send(command, cancellationToken);

            if (!isUpdated)
            {
                return NotFound($"Mașina cu ID-ul {id} nu a fost găsită.");
            }

            return NoContent();
        }
        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var car = await _mediator.Send(new GetCarByIdQuery(id), cancellationToken);

            if (car == null)
            {
                return NotFound($"Mașina cu ID-ul {id} nu a fost găsită.");
            }

            return Ok(car);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetCarsQuery query, CancellationToken cancellationToken)
        {
            var cars = await _mediator.Send(query, cancellationToken);
            return Ok(cars);
        }
    }
    
}
