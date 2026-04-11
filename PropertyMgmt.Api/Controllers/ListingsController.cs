using Microsoft.AspNetCore.Mvc;
using PropertyMgmt.Application.Features.Listings.Query.GetAllListings;
using PropertyMgmt.Application.Features.Listings.Commands.CreateListing;
using MediatR;

namespace PropertyMgmt.Api.Controllers;

public class ListingsController : BaseApiController
{
    // جلب البيانات (Query)
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetListingsWithPaginationQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    // إضافة عقار جديد (Command)
    [HttpPost]
    public async Task<IActionResult> Create(CreateListingCommand command)
    {
        var id = await Mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id }, id);
    }
}