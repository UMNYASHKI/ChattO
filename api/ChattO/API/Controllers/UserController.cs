using API.DTOs.Paging;
using API.DTOs.Requests.User;
using API.DTOs.Responses.File;
using API.DTOs.Responses.Organization;
using API.DTOs.Responses.User;
using API.DTOs.Sorting;
using API.Helpers;
using Application.AppUsers.Commands;
using Application.Helpers;
using Application.Users.Queries;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UserController : BaseController
{
    // Create a new users (admin or user) within an organization
    //[Authorize(Roles = $"{RolesConstants.SuperAdmin}, {RolesConstants.Admin}")]
    [HttpPost]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CreateAccountsRequest request)
    {
        var result = await Mediator.Send(Mapper.Map<CreateAppUser.Command>(request));
        if(!result.IsSuccessful)
            return HandleResult(result);

        return Ok();
    }

    // Get user by id
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
        response.Organization = Mapper.Map<GetDetailsOrganizationResponse>(userResult.Data.Organization);
        response.ProfileImage = Mapper.Map<FileResponse>(userResult.Data.ProfileImage);

        return Ok(response);
    }

    // Get all users (system admin permission)
    //groupId
    //organizationId
    //role
    //username
    //displayname
    //email
    //isEmailSent
    [Authorize]
    [HttpGet]
    [ProducesResponseType<PagingResponse<UserResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromQuery] UserFilterRequest request)
    {
        return Ok();
    }

    // Update user
    [Authorize(Roles = RolesConstants.User)]
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

    // Delete user
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
