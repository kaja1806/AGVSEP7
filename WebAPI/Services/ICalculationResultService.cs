using Shared.Models;

namespace WebAPI.Services;

public interface ICalculationResultService {
    Task<List<AdjustedCalculationDto>> GetCalculationResult(int? statorNo);
    Task<string> RunCalculationForSegment(int statorNo);
}