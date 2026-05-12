using System;
using MediatR;
using PropertyMgmt.Application.Common.Model;

namespace PropertyMgmt.Application.Features.Supscriptions.Query.GetAllSubscriptionPlan;

public record GetAllSubscriptionPlanQuery(int PageNumber, int PageSize) : IRequest<PaginatedList<AllSubscriptionPlanDto>>;

public class AllSubscriptionPlanDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
