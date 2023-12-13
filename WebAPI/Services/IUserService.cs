using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace WebAPI.Services;

public interface IUserService {
    Task<string> CreateUser(UserModelDto userModelDto);
    Task<IActionResult> LoginUser(UserModelDto userModelDto);
    Task<IActionResult> LogoutUser(string email);
    Task<UserModelDto?> GetUser(string email);
}