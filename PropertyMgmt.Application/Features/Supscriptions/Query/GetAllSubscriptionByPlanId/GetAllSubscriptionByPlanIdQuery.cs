using MediatR;
using PropertyMgmt.Application.Common.Model;

namespace PropertyMgmt.Application.Features.Supscriptions.Query.GetAllSubscriptionByPlanId
{
    public record GetAllSubscriptionByPlanIdQuery(Guid PlanId, int PageNumber, int PageSize) : IRequest<PaginatedList<SubscriptionDto>>;
}
