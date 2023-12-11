using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using WebAPI.Services;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpPost, Route("RegisterUser")]
    public async Task<ActionResult> RegisterUser([FromBody] UserModelDto userModelDto)
    {
        try
        {
            var result = await _userService.CreateUser(userModelDto);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost, Route("login/{email},{password}")]
    public async Task<ActionResult> LoginUser([FromRoute] string email, string password)
    {
        try
        {
            var userModel = new UserModelDto()
            {
                Email = email,
                Password = password
            };

            var getUser = await _userService.GetUser(email);

            if (getUser == null)
            {
                return new NotFoundObjectResult("User not found");
            }

            if (getUser.Token != null)
            {
                return new BadRequestObjectResult("User already logged in!");
            }

            var result = await _userService.LoginUser(userModel);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost, Route("logout/{email}")]
    public async Task<IActionResult> LogoutUser([FromRoute] string email)
    {
        try
        {
            var result = await _userService.LogoutUser(email);

            return Ok(result);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e);
        }
    }
}