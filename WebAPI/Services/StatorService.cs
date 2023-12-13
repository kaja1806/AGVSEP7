using Dapper;
using Database.Models;
using Database.SQLHelper;
using Shared.Models;

namespace WebAPI.Services;

public class StatorService : IStatorService {
    private readonly SqlConnectionClass _sqlConnectionClass;

    public StatorService(SqlConnectionClass sqlConnectionClass)
    {
        _sqlConnectionClass = sqlConnectionClass;
    }

    private async Task<List<StatorModel>> GetStatorDb()
    {
        string query =
            "SELECT Name, StatorNo, ProductionOrder, MeasurementNo, Operator, Date, StatorTemp, Finished FROM Stator";
        await using var connection = _sqlConnectionClass.GetConnection();
        var result = connection.Query<StatorModel>(query).ToList();
        return result;
    }

    public async Task<bool> SetNewStator(StatorDto statorDto)
    {
        try
        {
            await using var connection = _sqlConnectionClass.GetConnection();
            var query =
                $"INSERT INTO Stator(Name, StatorNo, ProductionOrder, MeasurementNo, Operator, Date, StatorTemp, Finished)" +
                $"VALUES ('{statorDto.Name}', {statorDto.StatorNo}, {statorDto.ProductionOrder}, {statorDto.MeasurementNo},'{statorDto.Operator}', '{statorDto.Date:yyyy-MM-dd}', {statorDto.StatorTemp}, 0)";
            await connection.QuerySingleOrDefaultAsync(query);
            connection.Query($"UPDATE Segment SET StatorID = ST.ID FROM Segment S INNER JOIN dbo.Stator ST ON ST.StatorNo = {statorDto.StatorNo};");

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<string> SetStatorFinished(int statorNo)
    {
        try
        {
            await using var connection = _sqlConnectionClass.GetConnection();
            connection.Query($"UPDATE Stator SET Finished = 1 WHERE StatorNo = '{statorNo}'");
        }
        catch (Exception e)
        {
            return e.Message;
        }

        return $"Stator {statorNo} completed";
    }

    public async Task<List<StatorDto>> GetStator()
    {
        var statorResult = await GetStatorDb();
        var statorList = new List<StatorDto>();

        foreach (var statorResults in statorResult)
        {
            var statorDto = new StatorDto
            {
                Name = statorResults.Name,
                StatorNo = statorResults.StatorNo,
                ProductionOrder = statorResults.ProductionOrder,
                Operator = statorResults.Operator,
                Date = statorResults.Date,
                MeasurementNo = statorResults.MeasurementNo,
                StatorTemp = statorResults.StatorTemp,
                Status = statorResults.Finished
            };
            statorList.Add(statorDto);
        }

        return statorList;
    }
}