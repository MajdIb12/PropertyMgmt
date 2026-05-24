using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Application.Features.Conversations.Command.MarkConversationAsRead;

public record MarkConversationAsReadCommand(Guid ConversationId, Guid UserId) : IRequest<bool>, IChatRequest
{
    public class MarkConversationAsReadCommandHandler(IApplicationDbContext context) : IRequestHandler<MarkConversationAsReadCommand, bool>
    {
        public async Task<bool> Handle(MarkConversationAsReadCommand request, CancellationToken cancellationToken)
        {
            var rowsAffected = await context.ChatMessages
            .Where(cm => cm.ConversationId == request.ConversationId
                      && cm.SenderId != request.UserId   
                      && !cm.IsRead)                      // ⚡ 2. حدث فقط الرسائل غير المقروءة لتوفير طاقة السيرفر
            .ExecuteUpdateAsync(cm => cm.SetProperty(c => c.IsRead, true), cancellationToken);

            // إذا تم تحديث رسالة واحدة على الأقل، نرجع true
            return rowsAffected > 0;

        }
    }
}


