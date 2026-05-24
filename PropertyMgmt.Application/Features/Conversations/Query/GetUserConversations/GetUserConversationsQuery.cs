namespace PropertyMgmt.Application.Features.Conversations.Queries.GetConversations;

using MediatR;
using PropertyMgmt.Application.Common.Model;
using PropertyMgmt.Application.Common.Model.ChatDtos;

public record GetUserConversationsQuery(int PageNumber, int PageSize) : IRequest<PaginatedList<ConversationDto>>;
