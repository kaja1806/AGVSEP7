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

    private async Task<List<CalculationResultModel>> GetCalculationsFromDb(int? statorNo)
    {
        string? condition = null;
        if (statorNo != null)
        {
            condition = $"where StatorNo = '{statorNo}'";
        }

        string query =
            $"select CR.Date, MeasuredValue, Tolerance, S.SegmentNo, SegmentId, Deviation, Adjustment, ST.StatorNo from CalculationResult CR " +
            $"inner join dbo.Segment S on S.ID = CR.SegmentId " +
            $"inner join dbo.Stator ST on ST.ID = S.StatorID {condition}";
        await using var connection = _sqlConnectionClass.GetConnection();

        var result = connection
            .Query<CalculationResultModel>(query).ToList();
        return result;
    }

    public async Task<List<AdjustedCalculationDto>> GetCalculationResult(int? statorNo)
    {
        var calculationResult = await GetCalculationsFromDb(statorNo);
        var adjustedCalcList = new List<AdjustedCalculationDto>();

        foreach (var calculationResults in calculationResult)
        {
            var adjustedCalcDto = new AdjustedCalculationDto()
            {
                Date = calculationResults.Date,
                Tolerance = calculationResults.Tolerance,
                MeasuredValue = calculationResults.MeasuredValue,
                Deviation = calculationResults.Deviation,
                AdjustmentNeeded = calculationResults.Adjustment,
                SegmentNo = calculationResults.SegmentNo,
                StatorNo = calculationResults.StatorNo
            };
            adjustedCalcList.Add(adjustedCalcDto);
        }

        return adjustedCalcList;
    }


    public async Task<string> RunCalculationForSegment(int statorNo)
    {
        try
        {
            var calculationResult = await GetCalculationsFromDb(statorNo);

            await using var connection = _sqlConnectionClass.GetConnection();
            foreach (var calculationResults in calculationResult)
            {
                var toleranceBool = Math.Abs(calculationResults.MeasuredValue - calculationResults.Tolerance) > 0.4;

                string query =
                    $"UPDATE CalculationResult SET Deviation = {calculationResults.MeasuredValue - calculationResults.Tolerance}, Adjustment = '{toleranceBool}' WHERE SegmentId = '{calculationResults.SegmentId}'";
                await connection.ExecuteAsync(query);
            }

            return $"Table edited successfully";
        }
        catch (Exception e)
        {
            // You might want to return a more specific error message depending on your requirements
            return $"An unexpected error occurred: {e.Message}";
        }
    }
}