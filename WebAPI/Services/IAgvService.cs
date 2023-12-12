using Shared.Models;

namespace WebAPI.Controllers;

internal interface IAgvService
{
    Task<string> SaveAgvStatusLogs(List<AgvStatusModel> agvStatus);
}