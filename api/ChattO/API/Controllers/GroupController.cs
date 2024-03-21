using API.DTOs.Requests.Group;
using API.DTOs.Responses.Group;
using API.Helpers;
using Application.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Roles = $"{RolesConstants.SuperAdmin},{RolesConstants.Admin}")]
public class GroupController : BaseController
{
    // Create a new group within an organization
    [HttpPost]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CreateGroupRequest request)
    {        

        return Ok();
    }

    // Get group by id
    [HttpGet("{id}")]
    [ProducesResponseType<GroupResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(new GroupResponse());
    }

    // Get all groups (by filter) ?? sort by, order by
    [HttpGet]
    [ProducesResponseType<PagingResponse<GroupResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromQuery] GroupFilterRequest request)
    {
        return Ok();
    }

    // Update group
    [HttpPatch("{id}")]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] JsonPatchDocument<UpdateGroupRequest> request)
    {
        return Ok();
    }

    // Delete group
    [HttpDelete("{id}")]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        return Ok();
    }
}
