using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Application.Features.ListingImages.Command.DeleteImage;

public class DeleteListingImageHandler : IRequestHandler<DeleteListingImageCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IFileService _fileService;

    public DeleteListingImageHandler(IApplicationDbContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<bool> Handle(DeleteListingImageCommand request, CancellationToken ct)
    {
        var listing = await _context.Listings
            .Include(l => l.Images)
            .FirstOrDefaultAsync(l => l.Id == request.ListingId, ct)
            ?? throw new NotFoundException(nameof(Listings), request.ListingId);
        if (listing.Images.Count <= 1)
        {
            throw new DeleteFailureException("Cannot delete the only image of the listing. Please add another image before deleting this one.");
        }
        var imageToDelete = listing.Images.FirstOrDefault(i => i.Id == request.ImageId)
            ?? throw new NotFoundException(nameof(ListingImages), request.ImageId);

        bool wasPrimary = imageToDelete.IsPrimary;

        _fileService.DeleteImage(imageToDelete.ImageUrl);

        // 4. حذف السجل من قاعدة البيانات
        _context.ListingImages.Remove(imageToDelete);

        if (wasPrimary && listing.Images.Count > 1)
        {
            var nextPrimary = listing.Images.FirstOrDefault(i => i.Id != request.ImageId);
            if (nextPrimary != null)
            {
                nextPrimary.IsPrimary = true;
            }
        }

        return true;
    }
}