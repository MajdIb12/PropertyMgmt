using MediatR;
using Microsoft.AspNetCore.Http;

namespace PropertyMgmt.Application.Features.Listings.Commands.UploadImages;
    public record UploadListingImagesCommand(
    Guid ListingId, 
    IEnumerable<IFormFile> Files) : IRequest<bool>;
