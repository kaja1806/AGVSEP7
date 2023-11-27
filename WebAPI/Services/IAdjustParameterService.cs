using Shared.Models;

namespace WebAPI.Services;
public interface IAdjustParameterService
{
    string AdjustParameters(StatorModel realTimeData);
}
