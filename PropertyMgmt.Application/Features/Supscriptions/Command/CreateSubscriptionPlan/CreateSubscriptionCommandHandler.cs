using MediatR;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Application.Features.Supscriptions.Command.CreateSubscriptionPlan;

public class CreateSubscriptionCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateSubscriptionCommand, Guid>
{

    public async Task<Guid> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscriptionPlan = new SubscriptionPlan
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            MaxListings = request.MaxListings,
            Price = request.Price,
            DurationInMonths = request.DurationInMonths
        };

        await context.SubscriptionPlans.AddAsync(subscriptionPlan);
        return subscriptionPlan.Id;
    }
}
