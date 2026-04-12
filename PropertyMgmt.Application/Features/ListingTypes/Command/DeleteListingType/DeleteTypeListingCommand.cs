using MediatR;

namespace PropertyMgmt.Application.Features.ListingTypes.Command.DeleteListingType
{
    public record DeleteListingTypeCommand(Guid Id) : IRequest<bool>;
}