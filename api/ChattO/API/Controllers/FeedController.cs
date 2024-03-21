using API.DTOs.Requests.Feed;
using API.DTOs.Responses.Feed;
using API.Helpers;
using Application.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Roles = RolesConstants.User)]
public class FeedController : BaseController
{
    // Create a new feed based on groupId of users or partial set of users (list of userIds)   
    [HttpPost]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CreateFeedRequest request)
    {
        return Ok();
    }

    // Get feed by id
    [HttpGet("{id}")]
    [ProducesResponseType<FeedResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok();
    }

    // Update feed
    [HttpPatch("{id}")]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] JsonPatchDocument<UpdateFeedRequest> request)
    {
        return Ok();
    }

    // Delete feed
    [HttpDelete("{id}")]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        return Ok();
    }

    // Get all feeds (by filter) ?? sort by, order by
    [HttpGet]
    [ProducesResponseType<PagingResponse<FeedResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromQuery] FeedFilterRequest request)
    {
        return Ok();
    }
}
