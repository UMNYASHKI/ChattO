using API.DTOs.Requests.Feed;
using API.DTOs.Responses.Feed;
using API.Extensions;
using API.Helpers;
using Application.Feeds.Commands;
using Application.Feeds.Queries;
using Application.Helpers;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Roles = RolesConstants.User)]
public class FeedController : BaseController
{
    [HttpPost]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CreateFeedRequest request)
    {
        var command = Mapper.Map<CreateFeed.Command>(request);
        command.CreatorId = User.GetIdFromPrincipal();
        var result = await Mediator.Send(command);
        if (!result.IsSuccessful)
            return HandleResult(result);

        return CreatedAtAction(nameof(this.GetById), new { id = result.Data.Id}, Mapper.Map<FeedResponse>(result.Data));
    }

    [HttpGet("{id}")]
    [ProducesResponseType<FeedResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await Mediator.Send(new GetDetailsFeed.Query { Id = id });
        if (!result.IsSuccessful)
            return HandleResult(result);

        return Ok(Mapper.Map<FeedResponse>(result.Data));
    }

    [HttpPatch("{id}")]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] JsonPatchDocument<UpdateFeedRequest> request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var feedRequest = new UpdateFeedRequest();
        request.ApplyTo(feedRequest, ModelState);
        var feed = Mapper.Map<UpdateFeed.Command>(feedRequest);
        feed.Id = id;

        var result = await Mediator.Send(feed);
        if (!result.IsSuccessful)
            return HandleResult(result);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await Mediator.Send(new DeleteFeed.Command { Id = id });
        if (!result.IsSuccessful)
            return HandleResult(result);

        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType<PagingResponse<FeedResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromQuery] FeedFilterRequest request)
    {
        var listResult = await Mediator.Send(Mapper.Map<GetListFeeds.Query>(request));
        if (!listResult.IsSuccessful)
            return HandleResult(listResult);

        var response = new PagingResponse<FeedResponse>(listResult.Data.Items.Select(Mapper.Map<Feed, FeedResponse>),
              listResult.Data.TotalCount,
              listResult.Data.CurrentPage,
              listResult.Data.PageSize);

        return Ok(response);
    }

    [HttpGet("{id}/users")]
    [ProducesResponseType<List<FeedAppUserResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUsers(Guid id)
    {
        var result = await Mediator.Send(new GetFeedAppUsers.Query { FeedId = id });
        if (!result.IsSuccessful)
            return HandleResult(result);

        return Ok(result.Data.Select(Mapper.Map<FeedAppUserResponse>));
    }
}
