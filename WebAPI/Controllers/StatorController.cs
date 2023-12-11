using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using WebAPI.Services;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatorController : ControllerBase
{
    // making IStatorService visible for controller
    private readonly IStatorService _statorService;

    public StatorController(IStatorService statorService)
    {
        _statorService = statorService;
    }

    [HttpGet("GetStator")]
    public async Task<IActionResult> GetStator()
    {
        try
        {
            var result = await _statorService.GetStator();
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost("SetStator")]
    public async Task<IActionResult> SetStator(StatorDto statorDto)
    {
        try
        {
            var checkExistingStator = await _statorService.GetStator();
            var checkForSameNo = checkExistingStator.Any(x => x.StatorNo == statorDto.StatorNo);

            if (!checkForSameNo)
            {
                var setStatorDetails = await _statorService.SetNewStator(statorDto);
                if (setStatorDetails)
                {
                    return Ok($"Stator {statorDto.StatorNo} created successfully");
                }
                else
                {
                    //_logger.LogError("Failed to create Stator. SetNewStator returned false.");
                    return StatusCode(500, "Failed to create Stator");
                }
            }
            else
            {
                return BadRequest($"Stator {statorDto.StatorNo} already exists");
            }
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as needed
            //_logger.LogError(ex, "An unexpected error occurred during SetStator");
            return StatusCode(500, "Internal Server Error");
        }
    }
}