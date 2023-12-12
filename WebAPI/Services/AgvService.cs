using Database.SQLHelper;
using Shared.Models;
using WebAPI.Controllers;

namespace WebAPI.Services;

public class AgvService : IAgvService
{
    private readonly SqlConnectionClass _sqlConnectionClass;

    public AgvService(SqlConnectionClass sqlConnectionClass)
    {
        _sqlConnectionClass = sqlConnectionClass;
    }

    public async Task<string> SaveAgvStatusLogs(List<AgvStatusModel> agvStatusModel)
    {
        /*using (var connection = _sqlConnectionClass.GetConnection())
        {
            connection.QueryMultipleAsync(
                $"UPDATE AgvStatus SET Status = '{agvStatusModel.Status}'," +
                $"Action = '{agvStatusModel.Action}'," +
                $"Timestamp = '{agvStatusModel.Timestamp}'");


            return "Table edited successfully";
        }*/
        return null; //Not Done
    }
}