using API.DTOs.Requests.User;
using API.DTOs.Responses.File;
using API.DTOs.Responses.Organization;
using API.DTOs.Responses.User;
using API.Helpers;
using Application.AppUsers.Commands;
using Application.AppUsers.Queries;
using Application.Helpers;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UserController : BaseController
{
    [Authorize(Roles = $"{RolesConstants.SuperAdmin}, {RolesConstants.Admin}")]
    [HttpPost]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CreateAccountsRequest request)
    {
        var result = await Mediator.Send(Mapper.Map<CreateAppUser.Command>(request));
        if (!result.IsSuccessful)
            return HandleResult(result);

        return Ok();
    }

    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType<UserDetailsResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var userResult = await Mediator.Send(new GetDetailsAppUser.Query { Id = id });
        if (!userResult.IsSuccessful)
            return HandleResult(userResult);

        var response = Mapper.Map<UserDetailsResponse>(userResult.Data);

        return Ok(response);
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType<PagingResponse<UserResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromQuery] UserFilterRequest request)
    {
        var listResult = await Mediator.Send(Mapper.Map<GetListAppUsers.Query>(request));
        if (!listResult.IsSuccessful)
            return HandleResult(listResult);

        var response = new PagingResponse<UserResponse>(listResult.Data.Items.Select(Mapper.Map<AppUser, UserResponse>),
             listResult.Data.TotalCount,
             listResult.Data.CurrentPage,
             listResult.Data.PageSize);

        return Ok(response);
    }

    //[Authorize(Roles = RolesConstants.User)]
    [HttpPatch("{id}")]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] JsonPatchDocument<UpdateUserRequest> document)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var request = new UpdateUserRequest();
        document.ApplyTo(request, ModelState);
        var user = Mapper.Map<UpdateAppUser.Command>(request);
        user.Id = id;

        var updateResult = await Mediator.Send(user);
        if (!updateResult.IsSuccessful)
            return HandleResult(updateResult);

        return NoContent();
    }

    [Authorize(Roles = $"{RolesConstants.SystemAdmin},{RolesConstants.SuperAdmin},{RolesConstants.Admin}")]
    [HttpDelete("{id}")]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new DeleteAppUser.Command { Id = id });
        if (!result.IsSuccessful)
            return HandleResult(result);

        return NoContent();
    }
}
