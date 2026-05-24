using MediatR;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Application.Common.Model.ChatDtos;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Application.Features.Conversations.Command.SendMessageCommand;

public class SendMessageCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser) : IRequestHandler<SendMessageCommand, ChatMessageDto>
{
    public async Task<ChatMessageDto> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var conversation = await context.Conversations.FindAsync(request.ConversationId, cancellationToken)
            ?? throw new NotFoundException(nameof(Conversation), request.ConversationId);

        var senderId = Guid.TryParse(currentUser.UserId, out var parsedSenderId)
            ? parsedSenderId
            : throw new UnauthorizedAccessException();

        var sender = await context.Customers.FindAsync(senderId, cancellationToken)
            ?? throw new NotFoundException(nameof(User), senderId);

        var message = new ChatMessage 
        {
            Id = Guid.NewGuid(),
            ConversationId = request.ConversationId,
            SenderId = sender.Id,
            Content = request.Content,
            IsRead = false,
            TenantId = conversation.TenantId
        };

        context.ChatMessages.Add(message); 
        await context.SaveChangesAsync(cancellationToken);

        return new ChatMessageDto
        {
            Id = message.Id,
            ConversationId = message.ConversationId,
            SenderId = sender.Id,
            SenderName = sender.FullName,
            Content = message.Content,
            SentAt = message.CreatedAt,
            IsRead = message.IsRead
        };
    }
}
