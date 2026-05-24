using MediatR;
using PropertyMgmt.Application.Common.Model;
using PropertyMgmt.Application.Common.Model.ChatDtos;
using PropertyMgmt.Application.Interfaces;

namespace PropertyMgmt.Application.Features.Conversations.Query.GetConversationMessages;

public record GetConversationMessagesQuery(Guid ConversationId, int PageNumber, int PageSize) : IRequest<PaginatedList<ChatMessageDto>>, IChatRequest;
