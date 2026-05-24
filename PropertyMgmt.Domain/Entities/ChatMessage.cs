using PropertyMgmt.Domain.Common;

namespace PropertyMgmt.Domain.Entities;

public class ChatMessage : BaseEntity
{
    public Guid ConversationId { get; set; }
    public Guid SenderId { get; set; } 
    public string Content { get; set; } = string.Empty;
    public bool IsRead { get; set; }  = false;

    public Conversation Conversation { get; set; } = null!;
    public User Sender { get; set; } = null!;
}