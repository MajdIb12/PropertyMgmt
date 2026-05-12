using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Application.Features.Supscriptions.Query.GetSubscriptionPlanById;

public class GetSubscriptionPlanByIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetSubscriptionPlanByIdQuery, SubscriptionPlanDto>
{

    public async Task<SubscriptionPlanDto> Handle(GetSubscriptionPlanByIdQuery request, CancellationToken cancellationToken)
    {
        var subscriptionPlan = await context.SubscriptionPlans.FirstOrDefaultAsync(s => s.Id == request.Id)
            ?? throw new NotFoundException(nameof(SubscriptionPlan), request.Id);

        return new SubscriptionPlanDto
        {
            Id = subscriptionPlan.Id,
            Name = subscriptionPlan.Name,
            Description = subscriptionPlan.Description,
            MaxListings = subscriptionPlan.MaxListings,
            Price = subscriptionPlan.Price,
            DurationInMonths = subscriptionPlan.DurationInMonths
        };
    }
}
