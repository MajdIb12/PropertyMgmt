using Microsoft.AspNetCore.Mvc;
using PropertyMgmt.Application.Features.Listings.Query.GetAllListings;
using PropertyMgmt.Application.Features.Listings.Commands.CreateListing;
using Microsoft.AspNetCore.Authorization;
using PropertyMgmt.Application.Features.Listings.Query.GetListingById;
using PropertyMgmt.Application.Features.Listings.Commands.DeleteListing;
using PropertyMgmt.Application.Features.Listings.Commands.UploadImages;
using PropertyMgmt.Application.Features.Listings.Commands.DeleteImage;

namespace PropertyMgmt.Api.Controllers;

[Authorize] // تأمين العمليات بشكل عام
public class ListingsController : BaseApiController
{
    // 1. جلب قائمة العقارات مع الفلترة (Query)
    [HttpGet]
    [AllowAnonymous] // السماح للجميع بالتصفح
    [ProducesResponseType(typeof(ListingLookupDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListings([FromQuery] GetListingsWithPaginationQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    // 2. جلب تفاصيل عقار معين
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ListingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetListing(Guid id)
    {
        return Ok(await Mediator.Send(new GetListingByIdQuery(id)));
    }

    // 3. إنشاء عقار جديد (Command)
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateListingCommand command)
    {
        var id = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetListing), new { id }, id);
    }

    // 4. تحديث العقار
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateListingCommand command)
    {
        if (id != command.Id) return BadRequest("ID Mismatch");
        
        await Mediator.Send(command);
        return NoContent();
    }

    // 5. حذف العقار
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await Mediator.Send(new DeleteListingCommand(id));
        return NoContent();
    }

    // --- قسم إدارة الصور (Nested Resources) ---

    [HttpPost("{id:guid}/images")]
    public async Task<IActionResult> UploadImages(Guid id, [FromForm] List<IFormFile> files)
    {
        var result = await Mediator.Send(new UploadListingImagesCommand(id, files));
        return Ok(result);
    }

    [HttpDelete("{id:guid}/images/{imageId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteImage(Guid id, Guid imageId)
    {
       var result = await Mediator.Send(new DeleteListingImageCommand(id, imageId));
       return Ok(result);
    }
}