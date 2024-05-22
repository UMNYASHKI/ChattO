using API.DTOs.Requests.BillingInfo;
using API.DTOs.Responses.Billing;
using API.Helpers;
using Application.BillInfo.Commands;
using Application.BillInfo.Queries;
using Application.Helpers;
using Application.Payment.Commands;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BillingInfoController : BaseController
{
    [Authorize(Roles = RolesConstants.SystemAdmin)]
    [HttpPost]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CreateBillingInfoRequest request)
    {
        var result = await Mediator.Send(Mapper.Map<CreateBillingInfo.Command>(request));

        return HandleResult(result);
    }

    [Authorize(Roles = $"{RolesConstants.SystemAdmin},{RolesConstants.SuperAdmin}")]
    [HttpGet]
    [ProducesResponseType<PagingResponse<BillingInfoResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromQuery] GetBillingInfosRequest request)
    {
        var getResult = await Mediator.Send(Mapper.Map<Get.Query>(request));
        if (!getResult.IsSuccessful)
        {
            return HandleResult(getResult);
        }

        var data = getResult.Data;
        var items = data.Items.Select(Mapper.Map<BillingInfoResponse>);

        return HandleResult(Result.Success(new PagingResponse<BillingInfoResponse>() { Items = items, CurrentPage = data.CurrentPage, TotalCount = data.TotalCount, PageSize = data.PageSize, TotalPages = data.TotalPages }));
    }

    [Authorize(Roles = $"{RolesConstants.SystemAdmin},{RolesConstants.SuperAdmin}")]
    [HttpGet("{id}")]
    [ProducesResponseType<BillingInfoResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new GetById.Query() { Id = id });
        if (!result.IsSuccessful)
        {
            return HandleResult(Result.Failure<BillingInfoResponse>(result.Message));
        }

        return HandleResult(Result.Success(Mapper.Map<BillingInfoResponse>(result)));
    }
}
