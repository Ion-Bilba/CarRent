using CarRent.Application.Abstractions;
using CarRent.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CarRent.Application.Features.AdImages.Queries.GetImagesByAdIdQuery
{
    public class GetImagesByAdIdQueryHandler : IRequestHandler<GetImagesByAdIdQuery, List<AdImageDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetImagesByAdIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AdImageDto>> Handle(GetImagesByAdIdQuery request, CancellationToken cancellationToken)
        {

            var adExists = await _context.Ads
                .AnyAsync(a => a.Id == request.AdId, cancellationToken);

            if (!adExists)
            {
                throw new KeyNotFoundException($"Anunțul cu ID-ul {request.AdId} nu există.");
            }

            return await _context.AdImages
                .AsNoTracking()
                .Where(i => i.AdId == request.AdId)
                .OrderBy(i => i.DisplayOrder)
                .Select(i => new AdImageDto(
                    i.Id,
                    i.AdId,
                    i.ImageUrl,
                    i.DisplayOrder
                ))
                .ToListAsync(cancellationToken);
        }
    }
}