using Shared.Models;

namespace WebAPI.Services;

public interface ISegmentService
{
    Task<List<SegmentDto>> GetSegmentsForAGV(Guid statorId);

    Task<string> SetSegmentCoordinates(Guid segmentId, Coordinates segmentCoordinates);
}