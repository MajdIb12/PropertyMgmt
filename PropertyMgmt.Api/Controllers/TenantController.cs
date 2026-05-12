using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyMgmt.Application.Common.Model;
using PropertyMgmt.Application.Features.Tenants.Command.CreateTenant;
using PropertyMgmt.Application.Features.Tenants.Command.DeleteTenant;
using PropertyMgmt.Application.Features.Tenants.Command.UpdateTenant;
using PropertyMgmt.Application.Features.Tenants.Query.GetAllTenants;
using PropertyMgmt.Application.Features.Tenants.Query.GetTenantById;

namespace PropertyMgmt.Api.Controllers;

public class TenantController : BaseApiController
{
    /// <summary>
    /// Retrieves a paginated list of all tenants.
    /// </summary>
    /// <param name="query">The pagination query parameters.</param>
    /// <returns>An IActionResult containing the paginated list of tenants.</returns>
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(PaginatedList<TenantDto>),StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTenants([FromQuery] GetTenantsPaginationQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    /// <summary>
    /// Retrieves a specific tenant by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the tenant.</param>
    /// <returns>An IActionResult containing the tenant details.</returns>
    [AllowAnonymous]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TenantDto),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTenantById(string id)
    {
        return Ok(await Mediator.Send(new GetTenantByIdQuery(id)));
    }

    /// <summary>
    /// Updates an existing tenant with the provided details.
    /// </summary>
    /// <param name="id">The unique identifier of the tenant to update.</param>
    /// <param name="command">The update command containing new tenant data.</param>
    /// <returns>An IActionResult indicating the result of the update operation.</returns>
    [AllowAnonymous]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTenant(string id, [FromBody] UpdateTenantCommand command)
    {
        if (id != command.Id) return BadRequest("ID Mismatch");
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Deletes a tenant by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the tenant to delete.</param>
    /// <param name="command">The delete command containing tenant details.</param>
    /// <returns>An IActionResult indicating the result of the delete operation.</returns>
    [AllowAnonymous]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTenant(string id, [FromBody] DeleteTenantCommand command)
    {
        if (id != command.TenantId) return BadRequest("ID Mismatch");
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Creates a new tenant with the provided details.
    /// </summary>
    /// <param name="command">The create command containing tenant data.</param>
    /// <returns>An IActionResult with the created tenant's ID.</returns>
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTenant([FromBody] CreateTenantCommand command)
    {
        // Implementation for creating tenant
        var id = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetTenantById), new { id }, id);

    }
}