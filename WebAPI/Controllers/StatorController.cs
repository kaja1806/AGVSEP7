using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using WebAPI.Services;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatorController : ControllerBase
{
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
        catch (Exception ex)
        {
            return BadRequest($"Internal Server Error + {ex.Message}");
        }
    }

    [HttpPost("SetStatorFinished/{statorNo}")]
    public async Task<IActionResult> SetStatorFinished(int statorNo)
    {
        try
        {
            var result = await _statorService.SetStatorFinished(statorNo);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Internal Server Error + {ex.Message}");
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
                    return BadRequest("Failed to create Stator");
                }
            }
            else
            {
                return BadRequest($"Stator {statorDto.StatorNo} already exists");
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"Internal Server Error + {ex.Message}");
        }
    }
}