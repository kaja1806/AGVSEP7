using Shared.Models;

namespace WebAPI.Services;

public interface IAgvService {
    Task<string> SaveAgvStatusLogs(List<AgvStatusModel> agvStatus);
}