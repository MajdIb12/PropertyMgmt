using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using PropertyMgmt.Application.Features.Conversations.Command.SendMessageCommand;
using System.Security.Claims;

namespace PropertyMgmt.Api.Hubs;

[Authorize] // 🔒 لن يسمح لأي مستخدم بالاتصال إلا إذا كان يملك Token صالحاً
public class ChatHub(IMediator mediator) : Hub
{

    /// <summary>
    /// 🚪 يتم استدعاء هذه الدالة من الـ Frontend عندما يفتح المستخدم نافذة محادثة معينة
    /// </summary>
    public async Task JoinConversation(Guid conversationId)
    {
        string groupName = $"Conversation-{conversationId}";

        // نضع اتصال المستخدم الحالي داخل "جروب" خاص بهذه المحادثة فقط
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }

    /// <summary>
    /// 🏃‍♂️ يتم استدعاؤها عندما يغلق المستخدم نافذة المحادثة أو ينتقل لمحادثة أخرى
    /// </summary>
    public async Task LeaveConversation(Guid conversationId)
    {
        string groupName = $"Conversation-{conversationId}";

        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }

    /// <summary>
    /// 💬 الدالة الأساسية لاستقبال الرسالة الحية من أحد الأطراف وبثها للجميع
    /// </summary>
    public async Task SendMessage(Guid conversationId, string content)
    {
        var command = new SendMessageCommand(conversationId, content);
        var chatMessageDto = await mediator.Send(command);

        // 2️⃣ البث الحي: نرسل الـ DTO النظيف لكل المستخدمين المتواجدين حالياً في هذه الغرفة
        string groupName = $"Conversation-{conversationId}";

        // العميل (Frontend) سيستمع لدالة اسمها "ReceiveMessage" ليستقبل الـ DTO ويرسمه فوراً
        await Clients.Group(groupName).SendAsync("ReceiveMessage", chatMessageDto);
    }
}