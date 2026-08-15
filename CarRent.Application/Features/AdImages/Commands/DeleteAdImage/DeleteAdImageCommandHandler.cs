using CarRent.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CarRent.Application.Features.AdImages.Commands.DeleteAdImage
{
	public class DeleteAdImageCommandHandler : IRequestHandler<DeleteAdImageCommand, bool>
	{
		private readonly IApplicationDbContext _context;

		public DeleteAdImageCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Handle(DeleteAdImageCommand request, CancellationToken cancellationToken)
		{
			var image = await _context.AdImages
				.FirstOrDefaultAsync(i => i.Id == request.ImageId && i.AdId == request.AdId, cancellationToken);

			if (image == null) return false;

			_context.AdImages.Remove(image);
			await _context.SaveChangesAsync(cancellationToken);

			return true;
		}
	}
}