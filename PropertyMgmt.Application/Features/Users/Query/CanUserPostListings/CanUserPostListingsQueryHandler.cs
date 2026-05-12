using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Application.Features.Users.Query.CanUserPostListings;

public class CanUserPostListingsQueryHandler(IApplicationDbContext context) : IRequestHandler<CanUserPostListingsQuery, bool>
{
    public async Task<bool> Handle(CanUserPostListingsQuery request, CancellationToken cancellationToken)
{
    var now = DateTime.UtcNow;

    // استعلام واحد يرجع true/false مباشرة من قاعدة البيانات
    return await context.OwnerSubscriptions
        .AsNoTracking()
        .Where(s => s.OwnerId == request.UserId && s.IsActive && s.EndDate > now)
        .AnyAsync(s => s.Subscription.MaxListings > context.Listings.Count(l => l.OwnerId == s.OwnerId), 
                  cancellationToken);
}
}
