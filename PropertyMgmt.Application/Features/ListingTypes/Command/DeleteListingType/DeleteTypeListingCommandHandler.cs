using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Application.Features.ListingTypes.Command.DeleteListingType;

public class DeleteTypeListingCommandHandler : IRequestHandler<DeleteListingTypeCommand, bool>
{
    private readonly IApplicationDbContext _context;
    public DeleteTypeListingCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<bool> Handle(DeleteListingTypeCommand request, CancellationToken cancellationToken)
    {
        var listingType = await _context.ListingTypes.FirstOrDefaultAsync(lt => lt.Id == request.Id, cancellationToken)
        ?? throw new NotFoundException(nameof(ListingTypes), request.Id);

        _context.ListingTypes.Remove(listingType);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}