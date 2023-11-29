using Shared.Models;

namespace WebAPI.Services;

public interface IStatorService
{
    Task<List<StatorDto>> GetStator();
}