using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using WebAPI.Services;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdjustParameterController : ControllerBase
{
    private readonly IAdjustParameterService _adjustmentService;

    public AdjustParameterController(IAdjustParameterService adjustmentService)
    {
        _adjustmentService = adjustmentService;
    }

    [HttpPost("adjust-parameters")]
    public IActionResult AdjustParameters([FromBody] StatorModel realTimeData)
    {
        var result =_adjustmentService.AdjustParameters(realTimeData);
        return Ok(result);
    }
}
