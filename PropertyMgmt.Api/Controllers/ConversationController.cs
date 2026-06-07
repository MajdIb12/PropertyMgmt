using Microsoft.AspNetCore.Mvc;
using PropertyMgmt.Application.Common.Model;
using PropertyMgmt.Application.Common.Model.ChatDtos;
using PropertyMgmt.Application.Features.Conversations.Command.MarkConversationAsRead;
using PropertyMgmt.Application.Features.Conversations.Command.SendMessageCommand;
using PropertyMgmt.Application.Features.Conversations.Queries.GetConversations;
using PropertyMgmt.Application.Features.Conversations.Query.GetConversationMessages;
using System.Security.Claims;

namespace PropertyMgmt.Api.Controllers;

public class ConversationController : BaseApiController
{
    [HttpPost("{conversationId}/read")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> MarkAsRead(Guid conversationId, CancellationToken cancellationToken)
    {
        var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(nameIdentifier, out Guid userId))
        {
            return Unauthorized();
        }

        var command = new MarkConversationAsReadCommand(conversationId, userId);

        var result = await Mediator.Send(command, cancellationToken);

        if (!result)
        {
            return BadRequest(new { message = "Conversation dose not exist or it is already read it" });
        }

        return Ok();
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<ConversationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetUserConversations(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetUserConversationsQuery(pageNumber, pageSize);

        var result = await Mediator.Send(query, cancellationToken);

        return Ok(result);
    }


    [HttpGet("{conversationId}/messages")]
    [ProducesResponseType(typeof(PaginatedList<ChatMessageDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetConversationMessages(
        Guid conversationId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        // بناء الـ Query
        var query = new GetConversationMessagesQuery(conversationId, pageNumber, pageSize);

        var result = await Mediator.Send(query, cancellationToken);

        return Ok(result);
    }
}