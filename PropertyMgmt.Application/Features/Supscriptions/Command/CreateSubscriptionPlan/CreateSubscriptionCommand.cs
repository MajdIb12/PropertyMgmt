using System;
using MediatR;

namespace PropertyMgmt.Application.Features.Supscriptions.Command.CreateSubscriptionPlan;

public record CreateSubscriptionCommand(
    string Name,
    string Description,
    int MaxListings,
    decimal Price,
    int DurationInMonths
): IRequest<Guid>;
