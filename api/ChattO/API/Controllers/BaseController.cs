using AutoMapper;
using MediatR;
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

}
