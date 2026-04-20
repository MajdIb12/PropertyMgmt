using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Application.Features.ListingImages.Command.SetMainImage;

public class SetMainImageCommandHandler : IRequestHandler<SetMainImageCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public SetMainImageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(SetMainImageCommand request, CancellationToken ct)
{
    await _context.ListingImages
        .Where(i => i.ListingId == request.ListingId)
        .ExecuteUpdateAsync(s => s.SetProperty(i => i.IsPrimary, false), ct);

    var image = await _context.ListingImages
        .FirstOrDefaultAsync(i => i.Id == request.ImageId && i.ListingId == request.ListingId, ct)
        ?? throw new NotFoundException(nameof(ListingImages), request.ImageId);

    image.IsPrimary = true;
    
    return true; 
}
}