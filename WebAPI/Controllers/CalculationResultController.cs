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


    [HttpGet("GetCalculationResult/{statorId}")]
    public IActionResult GetCalculationResults([FromRoute] Guid statorId)
    {
        try
        {
            var results = _calculationResultService.GetCalculationResults(statorId);
            return Ok(results.Result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
    
    [HttpPost("SetCalculationResult/{statorId}")]

    public async Task<IActionResult> SetCalculationResult([FromRoute] Guid statorId)
    {
            try
            {
                var results = await _calculationResultService.SetCalculationResult(statorId);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
    }

    }
    