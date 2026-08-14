using CarRent.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using CarRent.Domain.Entities;

namespace CarRent.Application.Features.Ads.Commands.UpdateAdCommand
{
    public class UpdateAdCommandHandler : IRequestHandler<UpdateAdCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public UpdateAdCommandHandler(IApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<bool> Handle(UpdateAdCommand request, CancellationToken cancellationToken)
        {
            var ad = await _context.Ads
               .FirstOrDefaultAsync(C => C.Id==request.Id,cancellationToken);
            if(ad==null)
            {
                return false;
            }

            ad.Id = request.Id;
            ad.Title = request.Title;
            ad.Description = request.Description;
            ad.Price = request.Price;
            ad.AdType = request.AdType;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
