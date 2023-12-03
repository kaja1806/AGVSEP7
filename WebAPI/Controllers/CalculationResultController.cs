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


    [HttpGet("GetCalculationResult/{statorNo}")]
    public IActionResult GetCalculationResult([FromRoute] string statorNo)
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
    [HttpGet("GetAllCalculationResults")]
    public IActionResult GetAllCalculationResults()
    {
        try
        {
            var results = _calculationResultService.GetCalculationResult(null);
            return Ok(results.Result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
    
    [HttpPost("SetCalculationResult/{statorNo}")]

    public async Task<IActionResult> SetCalculationResult([FromRoute] string statorNo)
    {
            try
            {
                var results = await _calculationResultService.SetCalculationResult(statorNo);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
    }

    }
    