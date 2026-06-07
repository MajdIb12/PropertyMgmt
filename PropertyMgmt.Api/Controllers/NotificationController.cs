using Microsoft.AspNetCore.Mvc;
using PropertyMgmt.Application.Features.Notifications.Query.GetUnreadNotifications;

namespace PropertyMgmt.Api.Controllers;

public class NotificationController : BaseApiController
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUnreadNotifications(Guid id)
    {
        var result = await Mediator.Send(new GetUnreadNotificationsQuery(id));
        return Ok(result);
    }
}