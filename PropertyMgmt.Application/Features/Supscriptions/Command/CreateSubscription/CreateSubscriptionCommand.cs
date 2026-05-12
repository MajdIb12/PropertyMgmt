using MediatR;

namespace PropertyMgmt.Application.Features.Supscriptions.Command.CreateSubscription;

public record CreateSubscriptionCommand(Guid UserId, Guid SubscriptionPlanId): IRequest<Guid>;
