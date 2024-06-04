using API.DTOs.Requests.UserGroups;
using Application.UserGroups.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class UserGroupController : BaseController
{
    [HttpPost]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Add([FromBody]AddUsersToGroupRequest request) 
    {
        var result = await Mediator.Send(Mapper.Map<Add.Command>(request));

        return HandleResult(result);
    }

    [HttpDelete]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromBody] DeleteUsersFromGroupRequest request)
    {
        var deleteResult = await Mediator.Send(Mapper.Map<Delete.Command>(request));

        return HandleResult(deleteResult);
    }
}
