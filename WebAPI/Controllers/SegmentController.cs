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

    [HttpGet("GetSegments/{statorId}")]
    public async Task<IActionResult> GetSegments([FromRoute] Guid statorId)
    {
        try
        {
            var result = await _segmentService.GetSegmentsForAGV(statorId);
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

    [HttpGet("SendSegmentDataToAGV/{segmentId}")]
    public async Task<IActionResult> SendSegmentDataToAgv([FromRoute] Guid segmentId)
    {
        try
        {
            var result = await _segmentService.GetSegmentsForAGV(segmentId);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}