using Shared.Models;

namespace WebAPI.Services;

public interface IStatorService
{
    List<StatorModel> GetStatorModels();
}