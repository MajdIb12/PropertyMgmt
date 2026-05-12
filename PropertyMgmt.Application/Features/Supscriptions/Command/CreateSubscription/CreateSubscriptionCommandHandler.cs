using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Application.Features.Supscriptions.Command.CreateSubscription;

public class CreateSubscriptionCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateSubscriptionCommand, Guid>
{
    public async Task<Guid> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
{
    var plan = await context.SubscriptionPlans
        .FirstOrDefaultAsync(x => x.Id == request.SubscriptionPlanId, cancellationToken)
        ?? throw new NotFoundException(nameof(SubscriptionPlan), request.SubscriptionPlanId);

    var now = DateTime.UtcNow;
    var remainingTime = TimeSpan.Zero;

    // 1. جلب آخر اشتراك فعال لهذا المستخدم
    var lastSubscription = await context.OwnerSubscriptions
        .FirstOrDefaultAsync(x => x.OwnerId == request.UserId && x.IsActive, cancellationToken);

    if (lastSubscription != null)
    {
        if (lastSubscription.EndDate > now && plan.Price <= lastSubscription.Subscription.Price)
        {
            // حساب الفرق الزمني بين تاريخ الانتهاء والآن
            remainingTime = (TimeSpan)(lastSubscription.EndDate - now);
        }

        // إغلاق الاشتراك القديم
        lastSubscription.IsActive = false;
    }

    var newEndDate = now.AddMonths(plan.DurationInMonths).Add(remainingTime);

    var subscription = new OwnerSubscription
    {
        OwnerId = request.UserId,
        SubscriptionPlanId = request.SubscriptionPlanId,
        StartDate = now,
        EndDate = newEndDate,
        IsActive = true
    };

    await context.OwnerSubscriptions.AddAsync(subscription, cancellationToken);
    await context.SaveChangesAsync(cancellationToken);

    return subscription.Id;
}
}