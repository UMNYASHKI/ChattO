using AutoMapper;
using MediatR;
using Application.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Helpers;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ModelStateValidation]
public class BaseController : ControllerBase
{
    private IMediator _mediator;
    private IMapper _mapper;
    protected IMediator Mediator =>
        _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    protected IMapper Mapper =>
        _mapper ??= HttpContext.RequestServices.GetService<IMapper>();

    protected ActionResult HandleResult<T>(Result<T> result)
    {
        if (result == null) 
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        if (result.IsSuccessful && result.Data != null)
            return Ok(result.Data);

        if (result.IsSuccessful && result.Data == null)
            return NotFound();

        return BadRequest(result.Message);
    }
}
