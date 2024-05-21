using API.DTOs.Requests.Billing;
using API.DTOs.Responses.Billing;
using API.Helpers;
using Application.Abstractions;
using Application.Helpers;
using Application.Payment.Commands;
using Application.Payment.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BillingController : BaseController
{
    private readonly IUserService _userService;

    public BillingController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize(Roles = RolesConstants.Admin)]
    [HttpPost]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromQuery] Guid billingInfoId)
    {
        var user = await _userService.GetCurrentUser();
        var request = new CreateBillingRequest() { OrganizationId = user.Data.OrganizationId, BillingInfoId = billingInfoId };
        var result = await Mediator.Send(new CreateBilling.Command() { BillingInfoId = request.BillingInfoId, OrganizationId = request.OrganizationId });
        if (!result.IsSuccessful)
        {
            return HandleResult(result);
        }

        return Ok(result.Data);
    }

    [Authorize(Roles = $"{RolesConstants.SystemAdmin}, {RolesConstants.SuperAdmin}")]
    [HttpGet("{id}")]
    [ProducesResponseType<BillingResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new GetBillingById.Query() { Id = id });
        if (!result.IsSuccessful)
        {
            return HandleResult(Result.Failure<BillingResponse>(result.Message));
        }

        return HandleResult(Result.Success(Mapper.Map<BillingResponse>(result)));
    }

    [Authorize(Roles = $"{RolesConstants.SystemAdmin}, {RolesConstants.SuperAdmin}")]
    [HttpGet]
    [ProducesResponseType<PagingResponse<BillingResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromQuery] GetOrganizationBillingsRequest request)
    {
        var getResult = await Mediator.Send(Mapper.Map<GetBillingByOrganizationId.Query>(request));
        if (!getResult.IsSuccessful)
        {
            return HandleResult(Result.Failure<PagingResponse<BillingResponse>>(getResult.Message));
        }

        var data = getResult.Data;
        var items = data.Items.Select(Mapper.Map<BillingResponse>);

        return HandleResult(Result.Success(new PagingResponse<BillingResponse>() { Items = items, CurrentPage = data.CurrentPage, TotalCount = data.TotalCount, PageSize = data.PageSize, TotalPages = data.TotalPages }));
    }

    [Authorize(Roles = RolesConstants.Admin)]
    [HttpPut]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Capture([FromQuery] Guid billingId)
    {
        var result = await Mediator.Send(new CaptureBilling.Command() { BillingId = billingId });
        if (!result.IsSuccessful)
        {
            return HandleResult(result);
        }

        return Ok(result.Data);
    }
}
