using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using CarRent.Domain.Entities;
using CarRent.Domain.Enums;
using CarRent.Application.Abstractions;

namespace CarRent.Application.Features.Ads.Commands
{
    public class CreateAdCommandHandler : IRequestHandler<CreateAdCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateAdCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateAdCommand request, CancellationToken cancellationToken)
        {
            var ad = new Ad
            {
                Id = Guid.NewGuid(),
                CarId = request.CarId,
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                AdType = Enum.Parse<AdType>(request.AdType, ignoreCase: true)
            };
            _context.Ads.Add(ad);
            await _context.SaveChangesAsync(cancellationToken);
            return ad.Id;

        }
    }
}