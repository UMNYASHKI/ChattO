using API.Helpers;
using Application.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ModelStateValidation]
public class BaseController : ControllerBase
{
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
