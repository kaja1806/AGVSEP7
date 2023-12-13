using Shared.Models;

namespace WebAPI.Controllers;

public interface IAgvService
{
    Task<string> SaveAgvStatusLogs(List<AgvStatusModel> agvStatus);
}