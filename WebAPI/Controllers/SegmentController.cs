using Microsoft.AspNetCore.Mvc;
using Shared.Models;
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
            var result = await _segmentService.GetSegmentsForAGV(statorNo);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost("SetSegmentCoordinates/{segmentId},{coordinateX}, {coordinateY}")]
    public async Task<IActionResult> SetSegmentCoordinates([FromRoute] Guid segmentId, double coordinateX,
        double coordinateY)
    {
        try
        {
            var segmentCoordinates = new Coordinates()
            {
                LocationX = coordinateX,
                LocationY = coordinateY
            };
            var result = await _segmentService.SetSegmentCoordinates(segmentId, segmentCoordinates);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet("SendSegmentDataToAGV/{segmentNo}")]
    public async Task<IActionResult> SendSegmentDataToAgv([FromRoute] int segmentNo)
    {
        try
        {
            var result = await _segmentService.GetSegmentsForAGV(segmentNo);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}