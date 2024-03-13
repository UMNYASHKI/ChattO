using API.DTOs.Requests.Organization;
using API.DTOs.Responses.Organization;
using Application.Abstractions;
using Application.Organizations.Commands;
using Application.Organizations.Queries;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public async Task<IActionResult> Create([FromBody] CreateOrganizationRequest request)
    {
        var result = await Mediator.Send(Mapper.Map<CreateOrganization.Command>(request));

        if (!result.IsSuccessful)
            return HandleResult(result);

        var user = Mapper.Map<AppUser>(request);
        user.OrganizationId = result.Data;

        return HandleResult(await _userService.RegisterUserAsync(user));
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var organizationResult = await Mediator.Send(new GetDetailsOrganization.Query { Id = id });

        if (!organizationResult.IsSuccessful)
            return HandleResult(organizationResult);

        return Ok(Mapper.Map<GetDetailsOrganizationResponse>(organizationResult.Data));       
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await Mediator.Send(new DeleteOrganization.Command { Id = id });

        if (!result.IsSuccessful)
            return HandleResult(result);

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateOrganizationRequest request)
    {
        var organization = Mapper.Map<UpdateOrganization.Command>(request);
        organization.Id = id;

        var result = await Mediator.Send(organization);

        if(!result.IsSuccessful)
            return HandleResult(result);

        return NoContent();
    }

}
