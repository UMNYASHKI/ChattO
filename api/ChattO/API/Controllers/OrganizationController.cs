using API.DTOs.Requests.Orqanization;
using API.DTOs.Responses.Organization;
using Application.Organizations.Commands;
using Application.Organizations.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class OrganizationController : BaseController
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] CreateOrganizationRequest request)
    {
        //organization creation logic
        var result = await Mediator.Send(Mapper.Map<CreateOrganization.Command>(request));

        if (!result.IsSuccessful)
            return HandleResult(result);

        //super admin creation logic

        return Ok();
    }

    //[Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var organizationResult = await Mediator.Send(new GetDetailsOrganization.Query { Id = id });

        if (!organizationResult.IsSuccessful)
            return HandleResult(organizationResult);

        return Ok(Mapper.Map<GetDetailsOrganizationResponse>(organizationResult.Data));       
    }

    //[Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return HandleResult(await Mediator.Send(new DeleteOrganization.Command { Id = id }));
    }

    //[Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateOrganizationRequest request)
    {
        var organization = Mapper.Map<UpdateOrganization.Command>(request);
        organization.Id = id;

        return HandleResult(await Mediator.Send(organization));
    }

}
