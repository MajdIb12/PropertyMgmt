
using FluentValidation;
using MediatR;
using PropertyMgmt.Application.Common.Model;
using PropertyMgmt.Application.Common.Model.ChatDtos;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Application.Features.Conversations.Query.GetConversationMessages;

public class GetConversationMessagesQueryHandler(IApplicationDbContext context) : IRequestHandler<GetConversationMessagesQuery, PaginatedList<ChatMessageDto>>
{
    public async Task<PaginatedList<ChatMessageDto>> Handle(GetConversationMessagesQuery request, CancellationToken cancellationToken)
    {
        var query = context.ChatMessages
            .Where(cm => cm.ConversationId == request.ConversationId)
            .OrderByDescending(cm => cm.CreatedAt)
            .Select(cm => new ChatMessageDto
            {
                Id = cm.Id,
                ConversationId = cm.ConversationId,
                SenderId = cm.SenderId,
                SenderName = cm.Sender.FullName,
                Content = cm.Content,
                SentAt = cm.CreatedAt,
                IsRead = cm.IsRead,
            });
        return await PaginatedList<ChatMessageDto>.CreateAsync(query, request.PageNumber, request.PageSize, cancellationToken);
    }
}
