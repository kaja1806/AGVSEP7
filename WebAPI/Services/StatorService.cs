using Dapper;
using Database.Models;
using Database.SQLHelper;
using Shared.Models;

namespace WebAPI.Services;

public class StatorService : IStatorService
{
    private readonly SqlConnectionClass _sqlConnectionClass;

    public StatorService(SqlConnectionClass sqlConnectionClass)
    {
        _sqlConnectionClass = sqlConnectionClass;
    }

    private async Task<List<StatorModel>> GetStatorDb()
    {
        await using var connection = _sqlConnectionClass.GetConnection();
        List<StatorModel> result = connection.Query<StatorModel>(@"SELECT * FROM Stator").ToList();
        return result;
    }

    public async Task<bool> SetNewStator(StatorDto statorDto)
    {
        try
        {
            await using var connection = _sqlConnectionClass.GetConnection();
            var query =
                $"INSERT INTO Stator(Name, StatorNo, ProductionOrder, MeasurementNo, Operator, Date, StatorTemp)" +
                $"VALUES ('{statorDto.Name}', {statorDto.StatorNo}, {statorDto.ProductionOrder}, {statorDto.MeasurementNo},'{statorDto.Operator}', '{statorDto.Date}', {statorDto.StatorTemp})";
            await connection.QuerySingleOrDefaultAsync(query);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
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
                StatorTemp = statorResults.StatorTemp
            };
            statorList.Add(statorDto);
        }

        return statorList;
    }
}