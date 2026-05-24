namespace PropertyMgmt.Application.Features.Conversations.Queries.GetConversations;

using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyMgmt.Application.Common.Model;
using PropertyMgmt.Application.Common.Model.ChatDtos;
using PropertyMgmt.Application.Interfaces;

public class GetUserConversationsQueryHandler : IRequestHandler<GetUserConversationsQuery, PaginatedList<ConversationDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetUserConversationsQueryHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<PaginatedList<ConversationDto>> Handle(GetUserConversationsQuery request, CancellationToken cancellationToken)
    {
        // 🔒 جلب الهويات من مصدرها الموثوق داخل السيرفر لحماية النظام من التلاعب
        var userId = Guid.TryParse(_currentUserService.UserId, out Guid result) ? result : throw new UnauthorizedAccessException();

        var query = _context.Conversations.AsNoTracking()
            .Where(c => c.OwnerId == userId || c.CustomerId == userId) // 🎯 1. عزل أمني تام للـ Tenant
            .Select(c => new
            {
                Conversation = c,
                LastMessage = c.Messages.OrderByDescending(m => m.CreatedAt).FirstOrDefault()
            })
            .Select(x => new ConversationDto
            {
                Id = x.Conversation.Id,
                BookingId = x.Conversation.BookingId,

                // 🔄 تحديد الطرف الآخر بناءً على من طلب الاستعلام
                OtherPartyId = x.Conversation.OwnerId == userId ? x.Conversation.CustomerId : x.Conversation.OwnerId,

                // 🚀 استخدام الـ Navigation Properties لجعل الـ SQL يعمل JOIN نظيف وسريع
                OtherPartyName = x.Conversation.OwnerId == userId
                    ? x.Conversation.Customer.FullName
                    : x.Conversation.Owner.FullName,

                // 💬 تعبئة بيانات الرسالة الأخيرة بأعلى كفاءة ممكنة
                LastMessageContent = x.LastMessage != null ? x.LastMessage.Content : string.Empty,
                LastMessageSentAt = x.LastMessage != null ? x.LastMessage.CreatedAt : (DateTime?)null,
                LastMessageSenderId = x.LastMessage != null ? x.LastMessage.SenderId : (Guid?)null,

                // 🛑 حساب الرسائل غير المقروءة بشرط ألا يكون المستخدم الحالي هو من أرسلها
                UnreadMessagesCount = x.Conversation.Messages.Count(m => !m.IsRead && m.SenderId != userId)
            });

        // تنفيذ الـ Pagination الفعلي على قاعدة البيانات
        return await PaginatedList<ConversationDto>.CreateAsync(query, request.PageNumber, request.PageSize, cancellationToken);
    }
}
