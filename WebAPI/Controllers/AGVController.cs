using AGV.Helpers;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgvController : ControllerBase
{
    private IAgvService _agvService;

    [HttpPost("SaveAgvStatusLogs/{statorId}")]
    public async Task<IActionResult> SaveAgvStatusLogs(string statorId, [FromBody] string logJson)
    {
        // Deserialize the JSON log
        var logEntries = JsonHelper.DeserializeObject<List<AgvStatusModel>>(logJson);
        
        var result = await _agvService.SaveAgvStatusLogs(statorId,logEntries);
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