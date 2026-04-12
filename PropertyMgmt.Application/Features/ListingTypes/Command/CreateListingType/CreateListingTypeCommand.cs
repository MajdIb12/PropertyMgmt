using MediatR;

namespace PropertyMgmt.Application.Features.ListingTypes.Command.CreateListingType;
public class CreateListingTypeCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}