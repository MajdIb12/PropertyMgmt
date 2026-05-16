using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Application.Features.Listings.Query.CanUserPostListing;

public class CanUserPostListingQueryHandler(IApplicationDbContext context) : IRequestHandler<CanUserPostListingQuery, bool>
{
    public async Task<bool> Handle(CanUserPostListingQuery request, CancellationToken cancellationToken)
{
    var now = DateTime.UtcNow;

    return await context.OwnerSubscriptions
        .AsNoTracking()
        .Where(s => s.OwnerId == request.UserId && s.IsActive && s.EndDate > now)
        .AnyAsync(s => s.Subscription.MaxListings > context.Listings.Count(l => l.OwnerId == s.OwnerId),
                  cancellationToken);
}
}
