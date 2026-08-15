using CarRent.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CarRent.Application.Features.AdImages.Commands.UpdateAdImage
{
    public class UpdateAdImageCommandHandler : IRequestHandler<UpdateAdImageCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public UpdateAdImageCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateAdImageCommand request, CancellationToken cancellationToken)
        {
            var image = await _context.AdImages
                .FirstOrDefaultAsync(i => i.Id == request.ImageId && i.AdId == request.AdId, cancellationToken);

            if (image == null) return false;

            image.ImageUrl = request.Request.ImageUrl;
            image.DisplayOrder = request.Request.DisplayOrder;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}