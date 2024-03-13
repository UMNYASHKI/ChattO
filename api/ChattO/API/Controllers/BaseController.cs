using AutoMapper;
using MediatR;
﻿using Application.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
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
        if (result == null) return NotFound();

        if (result.IsSuccessful && result.Data != null)
            return Ok(result.Data);

        if (result.IsSuccessful && result.Data == null)
            return NotFound();

        return BadRequest(result.Message);
    }
}
