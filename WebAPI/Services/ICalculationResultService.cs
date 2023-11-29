using Shared.Models;

namespace WebAPI.Services;

public interface ICalculationResultService
{
    Task<List<AdjustedCalculationDto>> GetCalculationResults(Guid statorId);
    Task<string> SetCalculationResult(Guid statorId);
}