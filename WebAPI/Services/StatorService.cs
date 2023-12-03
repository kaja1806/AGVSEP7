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

    public async Task<List<StatorModel>> GetStatorDb()
    {
        await using var connection = _sqlConnectionClass.GetConnection();
        List<StatorModel> result = connection.Query<StatorModel>(@"SELECT * FROM Stator").ToList();
        return result;
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
                NdeRadius = statorResults.NdeRadius,
                MidRadius = statorResults.MidRadius,
                DeRadius = statorResults.DeRadius,
            };
            statorList.Add(statorDto);
        }

        return statorList;
    }
}