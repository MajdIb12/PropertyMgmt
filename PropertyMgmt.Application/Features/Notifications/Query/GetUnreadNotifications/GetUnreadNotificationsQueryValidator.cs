using FluentValidation;

namespace PropertyMgmt.Application.Features.Notifications.Query.GetUnreadNotifications;

public class GetUnreadNotificationsQueryValidator : AbstractValidator<GetUnreadNotificationsQuery>
{
    public GetUnreadNotificationsQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
    }
}