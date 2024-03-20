using API.DTOs.Paging;
using API.DTOs.Requests.User;
using API.DTOs.Responses.User;
using API.DTOs.Sorting;
using API.Helpers;
using Application.Helpers;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UserController : BaseController
{
    // Create a new users (admin or user) within an organization
    [Authorize(Roles = $"{RolesConstants.SuperAdmin}, {RolesConstants.Admin}")]
    [HttpPost]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CreateAccountsRequest request)
    {
        return Ok();//Validate (admin cannot create admins)
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
        return Ok();
    }

    // Get all users (system admin permission)
    //groupId
    //organizationId
    //role
    //username
    //displayname
    //email
    //isEmailSent
    [Authorize(Roles = RolesConstants.SystemAdmin)]
    [HttpGet("GetUsersInSystem")]
    [ProducesResponseType<PagingResponse<UserResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromQuery] UserFilterRequest request)
    {
        return Ok();
    }

    // Get all users (organization admin permission)
    //groupId
    //username
    //displayname
    //email
    //isEmailSent
    [Authorize(Roles = $"{RolesConstants.SuperAdmin},{RolesConstants.Admin}")]
    [HttpGet("GetUsersInsideOrganization")]
    [ProducesResponseType<PagingResponse<UserResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUsersInOrganization([FromQuery] UserFilterRequest request)
    {
        return Ok();
        //OrganizationId is admin's organizationId
        //Finding role is User
    }

    //Find users by name(for simple users)
    [Authorize(Roles = RolesConstants.User)]
    [HttpGet("GetUsersByName")]
    [ProducesResponseType<PagingResponse<UserResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByName([FromQuery]GetUsersByNameRequest request)
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
        return Ok();//Validate (user can update only himself)
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
        return Ok();
    }
}
