using CarRent.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.Application.Features.Ads.Commands.DeleteAd
{
    public class DeleteAdCommandHandler : IRequestHandler<DeleteAdCommand, bool>
    {
        
        private readonly IApplicationDbContext _context;
        public DeleteAdCommandHandler(IApplicationDbContext context)
        {
            _context= context;
        }
        
        public async Task<bool> Handle(DeleteAdCommand request, CancellationToken cancellationToken)
        {
            var ad = await _context.Ads.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if(ad==null)
            {
                return false;
            }

            _context.Ads.Remove(ad);

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
