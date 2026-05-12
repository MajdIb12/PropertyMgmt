using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Model;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Application.Features.Supscriptions.Query.GetAllSubscriptionPlan;

public class GetAllSubscriptionPlanQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAllSubscriptionPlanQuery, PaginatedList<AllSubscriptionPlanDto>>
{
    public async Task<PaginatedList<AllSubscriptionPlanDto>> Handle(GetAllSubscriptionPlanQuery request, CancellationToken cancellationToken)
    {
        var subscriptionPlans = context.SubscriptionPlans.AsNoTracking()
            .Select(sp => new AllSubscriptionPlanDto
            {
                Id = sp.Id,
                Name = sp.Name,
                Description = sp.Description
            });

        return await PaginatedList<AllSubscriptionPlanDto>.CreateAsync(subscriptionPlans, request.PageNumber, request.PageSize, cancellationToken);
    }
}
