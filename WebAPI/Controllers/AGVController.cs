using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgvController : ControllerBase
{
    private IAgvService _agvService;

    [HttpPost("SaveAgvStatusLogs/{statorId}")]
    public async Task<IActionResult> SaveAgvStatusLogs([FromBody] List<AgvStatusModel> agvStatus)
    {
        var result = await _agvService.SaveAgvStatusLogs(agvStatus);
        return Ok(result);
    }

    [HttpGet("GetAgvStatusLog/{statorId}")]
    public async Task<IActionResult> GetAgvStatusLog(Guid statorId)
    {
        // Retrieve the log entries from the database based on the statorId
        // ...

        return Ok();
    }
}