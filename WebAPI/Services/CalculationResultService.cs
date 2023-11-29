using Dapper;
using Database.Models;
using Database.SQLHelper;
using Shared.Models;

namespace WebAPI.Services;

public class CalculationResultService : ICalculationResultService
{
    private readonly SqlConnectionClass _sqlConnectionClass;

    public CalculationResultService(SqlConnectionClass sqlConnectionClass)
    {
        _sqlConnectionClass = sqlConnectionClass;
    }

    public async Task<List<CalculationResultModel>> GetCalculationsFromDb(Guid statorId)
    {
        string query =
            $"select Date, MeasuredValue, Tolerance, SegmentId, S.SegmentNo from CalculationResult" +
            $" inner join dbo.Segment S on S.ID = CalculationResult.SegmentId where StatorID = '{statorId}'";
        using var connection = _sqlConnectionClass.GetConnection();

        var result = connection
            .Query<CalculationResultModel>(query).ToList();
        return result;
    }

    public async Task<List<AdjustedCalculationDto>> GetCalculationResults(Guid statorId)
    {
        var calculationResult = await GetCalculationsFromDb(statorId);
        var adjustedCalcList = new List<AdjustedCalculationDto>();

        foreach (var calculationResults in calculationResult)
        {
            var toleranceBool = Math.Abs(calculationResults.MeasuredValue - calculationResults.Tolerance) < 0.4;

            var adjustedCalcDto = new AdjustedCalculationDto()
            {
                Date = calculationResults.Date,
                Tolerance = calculationResults.Tolerance,
                MeasuredValue = calculationResults.MeasuredValue,
                Deviation = calculationResults.MeasuredValue - calculationResults.Tolerance,
                AdjustmentNeeded = toleranceBool,
                SegmentNo = calculationResults.SegmentNo
            };
            adjustedCalcList.Add(adjustedCalcDto);
        }

        return adjustedCalcList;
    }

    public async Task<string> SetCalculationResult(Guid statorId)
    {
        var calculationResult = await GetCalculationsFromDb(statorId);

        await using (var connection = _sqlConnectionClass.GetConnection())
        {
            foreach (var calculationResults in calculationResult)
            {
                var toleranceBool = Math.Abs(calculationResults.MeasuredValue - calculationResults.Tolerance) < 0.4;

                string query =
                    $"UPDATE CalculationResult SET Deviation = {calculationResults.MeasuredValue - calculationResults.Tolerance}, Adjustment = '{toleranceBool}' WHERE SegmentId = '{calculationResults.SegmentId}'";
                await connection.ExecuteAsync(query);
            }

            return $"Table edited successfully";
        }
    }
}