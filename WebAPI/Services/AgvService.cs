using Dapper;
using Database.SQLHelper;
using Shared.Models;
using WebAPI.Controllers;

namespace WebAPI.Services;

public class AgvService : IAgvService {
    private readonly SqlConnectionClass _sqlConnectionClass;

    public AgvService(SqlConnectionClass sqlConnectionClass)
    {
        _sqlConnectionClass = sqlConnectionClass;
    }

    public async Task<string> SaveAgvStatusLogs(List<AgvStatusModel> agvStatusModel)
    {
        try
        {
            foreach (var agvStatus in agvStatusModel)
            {
                string query =
                    $"insert into AGVStatus (Timestamp, AdjustmentCount, LogText, AgvNo, SegmentNo, StatorNo)" +
                    $"VALUES ('{agvStatus.AddedAt}','{null}','{agvStatus.LogText}','5005', '{agvStatus.SegmentNo}', {agvStatus.StatorNo})";

                await using var connection = _sqlConnectionClass.GetConnection();
                connection.Query(query);
            }

            return "Log added to database!";
        }
        catch (Exception e)
        {
            return $"An unexpected error occurred: {e.Message}";
        }
    }
}