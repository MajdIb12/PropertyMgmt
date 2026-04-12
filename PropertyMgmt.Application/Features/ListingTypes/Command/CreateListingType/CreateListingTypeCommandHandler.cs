using MediatR;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Application.Features.ListingTypes.Command.CreateListingType;
public class CreateListingTypeCommandHandler : IRequestHandler<CreateListingTypeCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateListingTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateListingTypeCommand request, CancellationToken cancellationToken)
    {
        var listingType = new ListingType
        {
            Name = request.Name,
            Description = request.Description
        };

        _context.ListingTypes.Add(listingType);
        await _context.SaveChangesAsync(cancellationToken);

        return listingType.Id;
    }
}