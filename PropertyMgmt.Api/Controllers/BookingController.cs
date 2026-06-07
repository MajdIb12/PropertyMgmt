using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyMgmt.Application.Features.Auth.Commands.Login;
using PropertyMgmt.Application.Features.Bookings.Command.CancelBooking;
using PropertyMgmt.Application.Features.Bookings.Command.ConfirmeBooking;
using PropertyMgmt.Application.Features.Bookings.Command.CreateBooking;
using PropertyMgmt.Application.Features.Bookings.Command.DeleteBooking;
using PropertyMgmt.Application.Features.Bookings.Query;
using PropertyMgmt.Application.Features.Bookings.Query.GetAllbookingByOwnerId;
using PropertyMgmt.Application.Features.Bookings.Query.GetAllBookingByUserId;
using PropertyMgmt.Application.Features.Bookings.Query.GetBookingById;

namespace PropertyMgmt.Api.Controllers;

public class BookingController : BaseApiController
{
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateBookingCommand command)
    {
        var id = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetBooking), new { id }, id);
    }


    [HttpPut("{id:guid}/Cancel")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Cancel(Guid id)
    {        
        await Mediator.Send(new CancelBookingCommand(id));
        return NoContent();
    }

    [HttpPut("{id:guid}/Confirm")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Confirm(Guid id)
    {        
        await Mediator.Send(new ConfirmeBookingCommand(id));
        return NoContent();
    }


    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await Mediator.Send(new DeleteBookingCommand(id));
        return NoContent();
    }


    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BookingDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetBooking(Guid id)
    {
        return Ok(await Mediator.Send(new GetBookingByIdQuery(id)));
    }

    [HttpGet("User")]
    [ProducesResponseType(typeof(BookingListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetBookingsByOwnerId(GetAllBookingByOwnerIdQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("Owner")]
    [ProducesResponseType(typeof(BookingListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetBookingsByUserId(GetAllBookingByUserIdQuery query)
    {
        return Ok(await Mediator.Send(query));
    }


}