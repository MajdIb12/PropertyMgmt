using FluentValidation;

namespace PropertyMgmt.Application.Features.Conversations.Command.SendMessageCommand;

public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
{
    public SendMessageCommandValidator()
    {
        RuleFor(x => x.ConversationId).NotEmpty().WithMessage("Conversation ID is required.");
        RuleFor(x => x.Content).NotEmpty().WithMessage("Message content cannot be empty.");
    }
}
