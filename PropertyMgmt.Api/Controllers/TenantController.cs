using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyMgmt.Application.Common.Model;
using PropertyMgmt.Application.Features.Tenants.Query.GetAllTenants;
using PropertyMgmt.Application.Features.Tenants.Query.GetTenantById;

namespace PropertyMgmt.Api.Controllers;

public class TenantController : BaseApiController
{
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(PaginatedList<TenantDto>),StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTenants([FromQuery] GetTenantsPaginationQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}