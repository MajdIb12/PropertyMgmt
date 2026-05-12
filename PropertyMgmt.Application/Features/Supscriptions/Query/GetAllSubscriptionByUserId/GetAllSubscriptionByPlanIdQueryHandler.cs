using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Model;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Application.Features.Supscriptions.Query.GetAllSubscriptionByUserId
{
    public class GetAllSubscriptionByUserIdQueryHandler(IApplicationDbContext context): IRequestHandler<GetAllSubscriptionByUserIdQuery, PaginatedList<SubscriptionDto>>
    {
        public Task<PaginatedList<SubscriptionDto>> Handle(GetAllSubscriptionByUserIdQuery request, CancellationToken cancellationToken)
        {
            var query = context.OwnerSubscriptions.AsNoTracking()
                .Select(s => new SubscriptionDto
                {
                    Id = s.Id,
                    UserId = s.OwnerId,
                    UserName = s.Owner.UserName ?? string.Empty,
                    SubsciptionPlanId = s.SubscriptionPlanId,
                    SubsciptionPlanName = s.Subscription.Name
                });
            return PaginatedList<SubscriptionDto>.CreateAsync(query,request.PageNumber, request.PageSize, cancellationToken);
        }

    }
}
