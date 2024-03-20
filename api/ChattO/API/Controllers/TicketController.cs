using API.DTOs.Requests.Billing;
using API.DTOs.Requests.Ticket;
using API.DTOs.Responses.Ticket;
using API.Helpers;
using Application.Helpers;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class TicketController : BaseController
{
    // Create a new ticket within an organization
    [Authorize(Roles = RolesConstants.User)]
    [HttpPost]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CreateTicketRequest request)
    {
        return Ok();
    }

    // Get ticket by id
    [Authorize(Roles = $"{RolesConstants.Admin},{RolesConstants.User}")]
    [HttpGet("{id}")]
    [ProducesResponseType<TicketDetailsResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        return Ok();
    }

    // Get all tickets (by filter)
    [Authorize(Roles = $"{RolesConstants.Admin},{RolesConstants.User}")]
    [HttpGet]
    [ProducesResponseType<PagingResponse<TicketResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromQuery] TicketFilterRequest request)
    {
        return Ok();
    }

    // Update ticket
    [Authorize(Roles = $"{RolesConstants.Admin},{RolesConstants.User}")]
    [HttpPatch("{id}")]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] JsonPatchDocument<UpdateTicketRequest> document)
    {
        return Ok();
    }

    // Delete ticket
    [Authorize(Roles = $"{RolesConstants.Admin},{RolesConstants.User}")]
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
