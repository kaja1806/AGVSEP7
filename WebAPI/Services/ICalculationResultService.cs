using Shared.Models;

namespace WebAPI.Services;

public interface ICalculationResultService
{
    Task<List<AdjustedCalculationDto>> GetCalculationResult(string? statorNo);
    Task<string> SetCalculationResult(string statorNo);
}