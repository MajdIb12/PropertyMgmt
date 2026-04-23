using MediatR;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Application.Features.Listings.Commands.DeleteImage;
public record DeleteListingImageCommand(Guid ListingId, Guid ImageId) 
    : IRequest<bool>, ITransactionalRequest;
