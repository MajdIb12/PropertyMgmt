using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Application.Common.Exceptions;
namespace PropertyMgmt.Application.Features.Listings.Commands.CreateListing;

public class UpdateListingCommandHandler : IRequestHandler<UpdateListingCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly UpdateListingMapper _mapper; // إضافة الماپر هنا

    public UpdateListingCommandHandler(IApplicationDbContext context, UpdateListingMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(UpdateListingCommand request, CancellationToken cancellationToken)
    {
       var listing = await _context.Listings.FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken);
       if (listing == null)
       {
           throw new NotFoundException("Listing", request.Id);
       }
       _mapper.MapToExistingEntity(request, listing);
       await _context.SaveChangesAsync(cancellationToken);
       return listing.Id;

    }
}