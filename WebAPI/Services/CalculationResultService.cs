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

    public async Task<List<CalculationResultModel>> GetCalculationsFromDb(string? statorNo)
    {
        string? condition = null;
        if (statorNo != null)
        {
            condition = $"where StatorNo = '{statorNo}'";
        }

        string query =
            $"select CR.Date, MeasuredValue, Tolerance, S.SegmentNo, SegmentId, ST.StatorNo from CalculationResult CR " +
            $"inner join dbo.Segment S on S.ID = CR.SegmentId " +
            $"inner join dbo.Stator ST on ST.ID = S.StatorID {condition}";
        using var connection = _sqlConnectionClass.GetConnection();

        var result = connection
            .Query<CalculationResultModel>(query).ToList();
        return result;
    }

    public async Task<List<AdjustedCalculationDto>> GetCalculationResult(string? statorNo)
    {
        var calculationResult = await GetCalculationsFromDb(statorNo);
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
                SegmentNo = calculationResults.SegmentNo,
                StatorNo = calculationResults.StatorNo
            };
            adjustedCalcList.Add(adjustedCalcDto);
        }

        return adjustedCalcList;
    }


    public async Task<string> SetCalculationResult(string statorNo)
    {
        try
        {
            var calculationResult = await GetCalculationsFromDb(statorNo);

            await using (var connection = _sqlConnectionClass.GetConnection())
            {
                foreach (var calculationResults in calculationResult)
                {
                    var toleranceBool = Math.Abs(calculationResults.MeasuredValue - calculationResults.Tolerance) > 0.4;

                    string query =
                        $"UPDATE CalculationResult SET Deviation = {calculationResults.MeasuredValue - calculationResults.Tolerance}, Adjustment = '{toleranceBool}' WHERE SegmentId = '{calculationResults.SegmentId}'";
                    await connection.ExecuteAsync(query);
                }

                return $"Table edited successfully";
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}