using Dapper;
using Database.SQLHelper;
using Shared.Models;

namespace WebAPI.Services;

public class SegmentService : ISegmentService
{
    private readonly SqlConnectionClass _sqlConnectionClass;

    public SegmentService(SqlConnectionClass sqlConnectionClass)
    {
        _sqlConnectionClass = sqlConnectionClass;
    }


    public async Task<List<SegmentModel>> GetSegments(Guid statorId)
    {
        await using (var connection = _sqlConnectionClass.GetConnection())
        {
            var result = connection
                .Query<SegmentModel>(
                    $"SELECT SegmentNo,LocationX,LocationZ,LocationY FROM Segment WHERE StatorID = '{statorId}'")
                .ToList();

            return result;
        }
    }

    public async Task<string> SetSegmentCoordinates(Guid segmentId, Coordinates segmentCoordiantes)
    {
        await using (var connection = _sqlConnectionClass.GetConnection())
        {
            var segmentExist = connection
                .Query<SegmentModel>(
                    $"SELECT ID FROM Segment WHERE ID = '{segmentId}'").ToList();
            if (segmentExist.Count != 0)
            {
                await connection.QuerySingleOrDefaultAsync(
                    $"UPDATE Segment SET LocationX = '{segmentCoordiantes.LocationX}'," +
                    $"LocationY = '{segmentCoordiantes.LocationY}'," +
                    $"LocationZ = '{segmentCoordiantes.LocationZ}' WHERE ID = '{segmentId}'");
                return "Table edited succesfully";
            }

            return $"Edit unsuccesful: {segmentId} does not exist in database";
        }
    }
}