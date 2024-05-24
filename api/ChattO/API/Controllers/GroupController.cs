using API.DTOs.Requests.Group;
using API.DTOs.Responses.Billing;
using API.DTOs.Responses.Group;
using API.Helpers;
using Application.Groups.Commands;
using Application.Groups.Queries;
using Application.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Roles = $"{RolesConstants.SuperAdmin},{RolesConstants.Admin}")]
public class GroupController : BaseController
{
    [HttpPost]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CreateGroupRequest request)
    {
        var createResult = await Mediator.Send(Mapper.Map<Create.Command>(request));

        return HandleResult(createResult);
    }

    [HttpGet("{id}")]
    [ProducesResponseType<GroupResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var getResult = await  Mediator.Send(new GetById.Query() { Id =  id });
        if (!getResult.IsSuccessful)
        {
            return HandleResult(getResult);
        }

        return Ok(Mapper.Map<GroupResponse>(getResult.Data));
    }

    [HttpGet]
    [ProducesResponseType<PagingResponse<GroupResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromQuery] GroupFilterRequest request)
    {
        var getResult = await Mediator.Send(Mapper.Map<Get.Query>(request));
        if (!getResult.IsSuccessful)
        {
            return HandleResult(getResult);
        }

        var data = getResult.Data;
        var items = data.Items.Select(Mapper.Map<GroupResponse>);

        return HandleResult(Result.Success(new PagingResponse<GroupResponse>() { Items = items.ToList(), CurrentPage = data.CurrentPage, TotalCount = data.TotalCount, PageSize = data.PageSize, TotalPages = data.TotalPages }));
    }

    [HttpPut("{id}")]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] UpdateGroupRequest request)
    {
        var group = Mapper.Map<Update.Command>(request);
        group.Id = id;

        var updateResult = await Mediator.Send(group);

        return HandleResult(updateResult);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleteResult = await Mediator.Send(new Delete.Command() { Id = id });

        return HandleResult(deleteResult);
    }
}
