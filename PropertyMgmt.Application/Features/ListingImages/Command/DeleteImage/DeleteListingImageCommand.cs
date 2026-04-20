using MediatR;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Application.Features.ListingImages.Command.DeleteImage;
public record DeleteListingImageCommand(Guid ListingId, Guid ImageId) 
    : IRequest<bool>, ITransactionalRequest;
