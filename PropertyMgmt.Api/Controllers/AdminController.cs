using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyMgmt.Application.Features.Admins.Command.CreateAdmin;
using PropertyMgmt.Application.Features.Admins.Command.DeleteAdmin;
using PropertyMgmt.Application.Features.Admins.Command.UpdateAdmin;
using PropertyMgmt.Application.Features.Admins.Query.GetAdminById;
using PropertyMgmt.Application.Features.Admins.Query.GetAllAdmin;

namespace PropertyMgmt.Api.Controllers;

[Authorize]
public class AdminController : BaseApiController
{
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateAdminCommand command)
    {
        var id = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetAdmin), new { id }, id);
    }


    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(GetAdminByIdResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAdmin(Guid id)
    {
        return await Mediator.Send(new GetAdminByIdQuery(id)) is GetAdminByIdResponseDto admin
            ? Ok(admin)
            : NotFound();
    }

    [HttpGet]
    [ProducesResponseType(typeof(AdminDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAdmins([FromQuery] GetAllAdminsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPut("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAdminRoleCommand command)
    {
        if (id != command.Id) return BadRequest("ID Mismatch");

        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await Mediator.Send(new DeleteAdminCommand(id));
        return NoContent();
    }
}