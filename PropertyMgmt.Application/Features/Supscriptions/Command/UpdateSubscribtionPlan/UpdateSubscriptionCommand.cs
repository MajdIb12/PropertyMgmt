using System;
using MediatR;

namespace PropertyMgmt.Application.Features.Supscriptions.Command.UpdateSubscribtionPlan;

public record UpdateSubscriptionCommand(
    Guid Id,
    string Description,
    int MaxListings,
    decimal Price,
    int DurationInMonths
): IRequest<bool>;
