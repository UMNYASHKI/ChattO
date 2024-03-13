using API.DTOs.Requests.Orqanization;
using Application.Organizations.Commands;
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
        var result = await Mediator.Send(Mapper.Map<CreateOrganization.Command>(request));
        return Ok(result);
    }
}
