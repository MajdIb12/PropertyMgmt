using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Application.Features.Supscriptions.Command.UpdateSubscribtionPlan;

public class UpdateSubscriptionCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateSubscriptionCommand, bool>
{

    public async Task<bool> Handle(UpdateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscriptionPlan = await context.SubscriptionPlans.FirstOrDefaultAsync(s => s.Id == request.Id)
         ?? throw new NotFoundException(nameof(SubscriptionPlan), request.Id);

        subscriptionPlan.Description = request.Description;
        subscriptionPlan.MaxListings = request.MaxListings;
        subscriptionPlan.Price = request.Price;
        subscriptionPlan.DurationInMonths = request.DurationInMonths;

        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
