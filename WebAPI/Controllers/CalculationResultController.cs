using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/controller")]
public class CalculationResultController : ControllerBase
{
    private readonly ICalculationResultService _calculationResultService;

    public CalculationResultController(ICalculationResultService calculationResultService)
    {
        _calculationResultService = calculationResultService;
    }


    [HttpGet("GetCalculationResult/{statorNo?}")]
    public IActionResult GetCalculationResult([FromRoute] int? statorNo)
    {
        try
        {
            var results = _calculationResultService.GetCalculationResult(statorNo);
            return Ok(results.Result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    [HttpPost("RunCalculationForSegment/{statorNo}")]
    public async Task<IActionResult> RunCalculationForSegment([FromRoute] int statorNo)
    {
        try
        {
            var results = await _calculationResultService.RunCalculationForSegment(statorNo);
            return Ok(results);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
}