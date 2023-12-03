using System.Data.Common;
using System.Data.SqlClient;
using Dapper;
using Database.SQLHelper;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using WebAPI.Services;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatorController : ControllerBase
{
    // making IStatorService visible for controller
    public readonly IStatorService _statorService;

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
}