using MediatR;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Application.Features.ListingImages.Command.SetMainImage;

public record SetMainImageCommand(Guid ListingId, Guid ImageId) : IRequest<bool>, ITransactionalRequest;
