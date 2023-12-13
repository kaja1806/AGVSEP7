using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SegmentController : ControllerBase
{
    private readonly ISegmentService _segmentService;

    public SegmentController(ISegmentService segmentService)
    {
        _segmentService = segmentService;
    }

    [HttpGet("GetSegments/{statorNo}")]
    public async Task<IActionResult> GetSegments([FromRoute] int statorNo)
    {
        try
        {
            var result = await _segmentService.GetSegmentsForAgv(statorNo);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Internal Server Error + {ex.Message}");
        }
    }

    [HttpGet("SendSegmentDataToAGV/{segmentNo}")]
    public async Task<IActionResult> SendSegmentDataToAgv([FromRoute] int segmentNo)
    {
        try
        {
            var result = await _segmentService.GetSegmentsForAgv(segmentNo);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Internal Server Error + {ex.Message}");
        }
    }
}