using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyMgmt.Application.Common.Model.ChatDtos;

public class ConversationDto
{
    public Guid Id { get; set; }
    public Guid BookingId { get; set; }

    public Guid OtherPartyId { get; set; }
    public string OtherPartyName { get; set; } = string.Empty;

    public string LastMessageContent { get; set; } = string.Empty;
    public DateTime? LastMessageSentAt { get; set; }
    public Guid? LastMessageSenderId { get; set; }

    public int UnreadMessagesCount { get; set; }
}
