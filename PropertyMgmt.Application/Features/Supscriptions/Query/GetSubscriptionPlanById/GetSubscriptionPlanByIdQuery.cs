using System;
using MediatR;

namespace PropertyMgmt.Application.Features.Supscriptions.Query.GetSubscriptionPlanById;

public record GetSubscriptionPlanByIdQuery(Guid Id) : IRequest<SubscriptionPlanDto>;

public class SubscriptionPlanDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int MaxListings { get; set; }
    public decimal Price { get; set; }
    public int DurationInMonths { get; set; }
}
