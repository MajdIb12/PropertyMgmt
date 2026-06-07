using MediatR;
using PropertyMgmt.Application.Common.Exceptions;
using PropertyMgmt.Application.Interfaces;
using PropertyMgmt.Domain.Entities;

namespace PropertyMgmt.Application.Features.Conversations.Command.MarkConversationAsRead;

public partial record MarkConversationAsReadCommand(Guid ConversationId, Guid UserId) : IRequest<bool>, IChatRequest
{
}


