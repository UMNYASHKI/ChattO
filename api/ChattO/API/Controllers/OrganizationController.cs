using API.DTOs.Requests.Organization;
using API.DTOs.Responses.Feed;
using API.DTOs.Responses.Organization;
using API.Helpers;
using Application.Abstractions;
using Application.Helpers;
using Application.Organizations.Commands;
using Application.Organizations.Queries;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class OrganizationController : BaseController
{
    private readonly IUserService _userService;

    public OrganizationController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CreateOrganizationRequest request)
    {
        var result = await Mediator.Send(Mapper.Map<CreateOrganization.Command>(request));

        if (!result.IsSuccessful)
            return HandleResult(result);

        var user = Mapper.Map<AppUser>(request);
        user.OrganizationId = result.Data;

        var userResult = await _userService.RegisterUserAsync(user);
        if (!userResult.IsSuccessful)
            return HandleResult(userResult);

        return CreatedAtAction(nameof(this.GetById), new { id = result.Data }, result.Data); 
    }

    [Authorize(Roles = RolesConstants.SystemAdmin)]
    [HttpGet]
    [ProducesResponseType<PagingResponse<GetDetailsOrganizationResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromQuery] OrganizationFilteringRequest request)
    {
        var listResult = await Mediator.Send(Mapper.Map<GetListOrganizations.Query>(request));
        if (!listResult.IsSuccessful)
            return HandleResult(listResult);

        var response = new PagingResponse<GetDetailsOrganizationResponse>(listResult.Data.Items.Select(Mapper.Map<Organization, GetDetailsOrganizationResponse>), 
            listResult.Data.TotalCount, 
            listResult.Data.CurrentPage, 
            listResult.Data.PageSize);

        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType<GetDetailsOrganizationResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var organizationResult = await Mediator.Send(new GetDetailsOrganization.Query { Id = id });

        if (!organizationResult.IsSuccessful)
            return HandleResult(organizationResult);

        return Ok(Mapper.Map<GetDetailsOrganizationResponse>(organizationResult.Data));       
    }

    [HttpDelete("{id}")]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await Mediator.Send(new DeleteOrganization.Command { Id = id });

        if (!result.IsSuccessful)
            return HandleResult(result);

        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update(Guid id, [FromBody]UpdateOrganizationRequest request)
    {
        var organization = Mapper.Map<UpdateOrganization.Command>(request);
        organization.Id = id;

        var result = await Mediator.Send(organization);

        if(!result.IsSuccessful)
            return HandleResult(result);

        return NoContent();
    }

}
