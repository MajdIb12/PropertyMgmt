using MediatR;

namespace PropertyMgmt.Application.Features.ListingTypes.Command.UpdateListingType;

public class UpdateListingTypeCommand : IRequest<Guid>
{
    public Guid Id { get; }
    public string Name { get; } = string.Empty;
    public string Description { get; } = string.Empty;
}