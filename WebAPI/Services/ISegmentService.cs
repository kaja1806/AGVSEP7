using Shared.Models;

namespace WebAPI.Services;

public interface ISegmentService
{
    Task<List<SegmentDto>> GetSegmentsForAGV(int statorNo);

    Task<string> SetSegmentCoordinates(Guid segmentId, Coordinates segmentCoordinates);
}