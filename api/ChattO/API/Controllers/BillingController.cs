using API.DTOs.Requests.Billing;
using API.DTOs.Responses.Billing;
using API.Helpers;
using Application.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BillingController : BaseController
{
    //Create billing by organization
    [Authorize(Roles = RolesConstants.SuperAdmin)]
    [HttpPost]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CreateBillingRequest request)
    {
        return Ok();
    }

    //Get Billing by id
    [Authorize(Roles = $"{RolesConstants.SystemAdmin}, {RolesConstants.SuperAdmin}")]
    [HttpGet("{id}")]
    [ProducesResponseType<BillingResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        return Ok();
    }

    // Get organization billings by organizationid
    [Authorize(Roles = $"{RolesConstants.SystemAdmin}, {RolesConstants.SuperAdmin}")]
    [HttpGet]
    [ProducesResponseType<PagingResponse<BillingResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromQuery] GetOrganizationBillingsRequest request)
    {
        return Ok();
    }
}
