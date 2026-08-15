using CarRent.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Application.Features.Cars.Commands.DeleteCar
{
    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCarCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            var car = await _context.Cars
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (car == null)
            {
                return false;
            }

            var hasAds = await _context.Ads.AnyAsync(a => a.CarId == request.Id, cancellationToken);
            if (hasAds)
            {
                throw new InvalidOperationException("Nu poți șterge o mașină care are anunțuri active.");
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}