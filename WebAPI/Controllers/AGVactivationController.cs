
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AGVactivationController: ControllerBase

{


    [HttpPost]
    
    public IActionResult ReceiveToleranceSignal([FromBody] AGVActivation agvActivation)
    {
        if (agvActivation == null)
        {
            return BadRequest("Invalid data");
        }
        
        bool isToleranceOK = AGVActivation.IsToleranceOK;
        Console.WriteLine($"Received tolerance signal: IsToleranceOK = {isToleranceOK}");
        
        if (isToleranceOK)
        {
            // bool agvActivationResult = ActivateAGV();
            
            Console.WriteLine("OK signal received. No further action taken.");

            return Ok("Tolerance signal received. AGV activation initiated.");
        }
        // Handle the case when the tolerance is not OK
        Console.WriteLine("Tolerance signal received. No action taken.");

        return Ok("Tolerance signal received. No action taken.");
    }

    // private bool ActivateAGV()
    // {
    //     return true;
    // }

    
}