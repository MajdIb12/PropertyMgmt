using MediatR;
using Microsoft.AspNetCore.Http;

namespace PropertyMgmt.Application.Features.ListingImages.Command.UploadImages;
    public record UploadListingImagesCommand(
    Guid ListingId, 
    IFormFileCollection Files) : IRequest<bool>;
