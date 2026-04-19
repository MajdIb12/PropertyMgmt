using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Application.Features.ListingImages.Command.UploadImages;

public class UploadListingImagesCommandHandler : IRequestHandler<UploadListingImagesCommand, bool>
    {
        private readonly IFileService _fileService;
        private readonly IApplicationDbContext _context;

        public UploadListingImagesCommandHandler(IFileService fileService, IApplicationDbContext context)
        {
            _fileService = fileService;
            _context = context;
        }

        public async Task<bool> Handle(UploadListingImagesCommand request, CancellationToken cancellationToken)
        {
            var listing = await _context.Listings
                .Include(l => l.Images) 
                .FirstOrDefaultAsync(l => l.Id == request.ListingId, cancellationToken)
                ?? throw new NotFoundException(nameof(Listings), request.ListingId);

            foreach (var file in request.Files)
            {
                var imageUrl = await _fileService.UploadImageAsync(file.OpenReadStream(), file.FileName);

                // 2. إنشاء السجل
                var image = new ListingImage
                {
                    ListingId = request.ListingId,
                    ImageUrl = imageUrl,
                    publicId = null,
                    IsPrimary = !listing.Images.Any(i => i.IsPrimary) 
                };

                _context.ListingImages.Add(image);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }