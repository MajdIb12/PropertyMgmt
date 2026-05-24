namespace PropertyMgmt.Application.Features.Conversations.Queries.GetConversations;

using FluentValidation;

public class GetUserConversationsQueryValidator : AbstractValidator<GetUserConversationsQuery>
{
    public GetUserConversationsQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("Page number must be greater than 0.");
        RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("Page size must be greater than 0.");
    }
}