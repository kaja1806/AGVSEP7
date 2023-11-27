using Dapper;
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

    public List<StatorModel> GetStatorModels()
    {
        using var connection = _sqlConnectionClass.GetConnection();
        List<StatorModel> result = connection.Query<StatorModel>(@"SELECT * FROM Stator").ToList();
        return result;
    }
}