using Dapper;
using Database.Models;
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


    public async Task<List<SegmentModel>> GetSegmentsDb(Guid statorId)
    {
        string query = $"SELECT * FROM Segment WHERE StatorID = '{statorId}'";
        using var connection = _sqlConnectionClass.GetConnection();
        var result = connection
            .Query<SegmentModel>(query)
            .ToList();

        return result;
    }

    public async Task<List<SegmentDto>> GetSegmentsForAGV(Guid statorId)
    {
        var segmentResult = await GetSegmentsDb(statorId);
        var segmentList = new List<SegmentDto>();

        foreach (var segmentResults in segmentResult)
        {
            var segmentDto = new SegmentDto()
            {
                SegmentNo = segmentResults.SegmentNo,
                SegmentCoordinates = new Coordinates
                {
                    LocationX = segmentResults.LocationX,
                    LocationY = segmentResults.LocationY
                }
            };
            segmentList.Add(segmentDto);
        }

        return segmentList;
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
                    $"LocationY = '{segmentCoordiantes.LocationY}' WHERE ID = '{segmentId}'");
                return "Table edited succesfully";
            }

            return $"Edit unsuccesful: {segmentId} does not exist in database";
        }
    }
}