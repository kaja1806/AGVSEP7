using Shared.Models;

namespace WebAPI.Controllers;

internal interface IAgvService
{
    Task<string> SaveAgvStatusLogs(string statorId, List<AgvStatusModel> logEntries);
}