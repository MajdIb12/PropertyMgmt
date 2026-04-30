using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyMgmt.Application.Features.ListingTypes.Command.CreateListingType;
using PropertyMgmt.Application.Features.ListingTypes.Command.DeleteListingType;
using PropertyMgmt.Application.Features.ListingTypes.Command.UpdateListingType;
using PropertyMgmt.Application.Features.ListingTypes.Query.GetAllListingTypes;
using PropertyMgmt.Application.Features.ListingTypes.Query.GetListingTypeById;

namespace PropertyMgmt.Api.Controllers;

//[Authorize]
public class ListingTypesController : BaseApiController
{
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ListingTypeLookupDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetListingTypes([FromQuery] GetListingTypesWithPaginationQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ListingTypeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetListingType(Guid id)
    {
        return Ok(await Mediator.Send(new GetListingTypeByIdQuery(id)));
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateListingType([FromBody] CreateListingTypeCommand command)
    {
        var Id = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetListingType), new { id = Id }, Id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateListingType(Guid id, [FromBody] UpdateListingTypeCommand command)
    {
        if (id != command.Id) return BadRequest("ID Mismatch");
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteListingType(Guid id)
    {
        await Mediator.Send(new DeleteListingTypeCommand(id));
        return NoContent();
    }

}