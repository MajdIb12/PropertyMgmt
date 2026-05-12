using MediatR;
using PropertyMgmt.Application.Common.Model;

namespace PropertyMgmt.Application.Features.Supscriptions.Query.GetAllSubscriptionByUserId
{
    public record GetAllSubscriptionByUserIdQuery(Guid UserId, int PageNumber, int PageSize) : IRequest<PaginatedList<SubscriptionDto>>;
}
