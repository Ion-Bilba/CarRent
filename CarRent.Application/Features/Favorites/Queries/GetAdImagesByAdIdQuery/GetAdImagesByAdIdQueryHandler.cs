using CarRent.Application.Abstractions;
using CarRent.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CarRent.Application.Features.AdImages.Queries.GetAdImagesByAdId
{
    public class GetAdImagesByAdIdQueryHandler : IRequestHandler<GetAdImagesByAdIdQuery, List<AdImageDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAdImagesByAdIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AdImageDto>> Handle(GetAdImagesByAdIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.AdImages
                .AsNoTracking()
                .Where(i => i.AdId == request.AdId)
                .OrderBy(i => i.DisplayOrder)
                .Select(image => new AdImageDto(
                    image.Id,
                    image.AdId,
                    image.ImageUrl,
                    image.DisplayOrder
                ))
                .ToListAsync(cancellationToken);
        }
    }
}