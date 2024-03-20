using API.DTOs.Requests.Billing;
using API.DTOs.Requests.BillingInfo;
using API.DTOs.Responses.Billing;
using API.Helpers;
using Application.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BillingInfoController : BaseController
{
    // Create a new billing info
    [Authorize(Roles = RolesConstants.SystemAdmin)]
    [HttpPost]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CreateBillingInfoRequest request)
    {
        return Ok();
    }

    // Get billingInfos
    [Authorize(Roles = $"{RolesConstants.SystemAdmin},{RolesConstants.SuperAdmin}")]
    [HttpGet]
    [ProducesResponseType<PagingResponse<BillingInfoResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromQuery]GetBillingInfosRequest request)
    {
        return Ok();
    }

    // Get billingInfo by id
    [Authorize(Roles = $"{RolesConstants.SystemAdmin},{RolesConstants.SuperAdmin}")]
    [HttpGet("{id}")]
    [ProducesResponseType<BillingInfoResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        return Ok();
    }
}
