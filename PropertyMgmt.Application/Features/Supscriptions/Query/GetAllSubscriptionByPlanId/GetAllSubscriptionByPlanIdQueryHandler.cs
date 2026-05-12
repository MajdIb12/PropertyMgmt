using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Model;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Application.Features.Supscriptions.Query.GetAllSubscriptionByPlanId
{
    public class GetAllSubscriptionByPlanIdQueryHandler(IApplicationDbContext context): IRequestHandler<GetAllSubscriptionByPlanIdQuery, PaginatedList<SubscriptionDto>>
    {
        public Task<PaginatedList<SubscriptionDto>> Handle(GetAllSubscriptionByPlanIdQuery request, CancellationToken cancellationToken)
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
