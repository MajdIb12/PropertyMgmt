using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Application.Features.Listings.Commands.DeleteListing;

public class DeleteListingCommandHandler : IRequestHandler<DeleteListingCommand, bool>
{
    private IApplicationDbContext _context;
    public DeleteListingCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<bool> Handle(DeleteListingCommand request, CancellationToken cancellationToken)
    {
        var listing = await _context.Listings.FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken);
        if (listing == null)
        {
            throw new NotFoundException(nameof(Listing), request.Id);
        }

        _context.Listings.Remove(listing);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}