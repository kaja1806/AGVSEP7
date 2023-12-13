using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using WebAPI.Services;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgvController : ControllerBase {
    private readonly IAgvService _agvService;

    public AgvController(IAgvService agvService)
    {
        _agvService = agvService;
    }

    [HttpPost("SaveAgvStatusLogs")]
    public async Task<IActionResult> SaveAgvStatusLogs([FromBody] List<AgvStatusModel> agvStatus)
    {
        try
        {
            var result = await _agvService.SaveAgvStatusLogs(agvStatus);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Internal Server Error + {ex.Message}");

        }
    }
}