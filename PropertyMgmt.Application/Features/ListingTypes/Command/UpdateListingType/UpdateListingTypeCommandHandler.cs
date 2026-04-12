using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Application.Interfaces;
namespace PropertyMgmt.Application.Features.ListingTypes.Command.UpdateListingType;
public class UpdateListingTypeCommandHandler : IRequestHandler<UpdateListingTypeCommand, Guid>
{
    private readonly IApplicationDbContext _Context;

    public UpdateListingTypeCommandHandler(IApplicationDbContext context)
    {
        _Context = context;
    }

    public async Task<Guid> Handle(UpdateListingTypeCommand request, CancellationToken cancellationToken)
    {
        var listingType = await _Context.ListingTypes.FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken)
        ?? throw new NotFoundException("ListingType", request.Id);

        listingType.Name = request.Name;
        listingType.Description = request.Description;

        _Context.ListingTypes.Update(listingType);
        await _Context.SaveChangesAsync(cancellationToken);

        return listingType.Id;
    }
}