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


    private async Task<List<SegmentModel>> GetSegmentsDb(int statorNo)
    {
        string query =
            $"SELECT LocationX, LocationY, Installed, SegmentNo, S.StatorNo FROM Segment inner join dbo.Stator S on S.ID = Segment.StatorID WHERE S.StatorNo = '{statorNo}'";
        await using var connection = _sqlConnectionClass.GetConnection();
        var result = connection
            .Query<SegmentModel>(query)
            .ToList();

        return result;
    }

    public async Task<List<SegmentDto>> GetSegmentsForAGV(int statorNo)
    {
        var segmentResult = await GetSegmentsDb(statorNo);
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

    public async Task<string> SetSegmentCoordinates(Guid segmentId, Coordinates segmentCoordinates)
    {
        await using var connection = _sqlConnectionClass.GetConnection();
        var segmentExist = connection
            .Query<SegmentModel>(
                $"SELECT ID FROM Segment WHERE ID = '{segmentId}'").ToList();
        if (segmentExist.Count != 0)
        {
            await connection.QuerySingleOrDefaultAsync(
                $"UPDATE Segment SET LocationX = '{segmentCoordinates.LocationX}'," +
                $"LocationY = '{segmentCoordinates.LocationY}' WHERE ID = '{segmentId}'");
            return "Table edited successfully";
        }

        return $"Edit unsuccessful: {segmentId} does not exist in database";
    }
}