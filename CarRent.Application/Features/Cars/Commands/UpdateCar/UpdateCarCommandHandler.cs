using CarRent.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Features.Cars.Commands.UpdateCar
{
   public class UpdateCarCommandHandler :IRequestHandler<UpdateCarCommand,bool>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCarCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateCarCommand request,CancellationToken cancellationToken)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            if(car==null)
            {
                return false;
            }
            car.Brand= request.Brand;
            car.Model = request.Model;
            car.Year = request.Year;
            car.Color = request.Color;
            car.Mileage = request.Mileage;
            car.FuelType = request.FuelType;
            car.EngineCapacity = request.EngineCapacity;
            car.Transmission = request.Transmission;
            car.TractionType = request.TractionType;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
