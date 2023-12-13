using Shared.Models;

namespace WebAPI.Services;

public interface IStatorService {
    Task<List<StatorDto>> GetStator();
    Task<bool> SetNewStator(StatorDto statorDto);
    Task<string> SetStatorFinished(int statorNo);
}