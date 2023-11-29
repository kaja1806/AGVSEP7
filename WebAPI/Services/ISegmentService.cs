using Shared.Models;

namespace WebAPI.Services;

public interface ISegmentService
{
    Task<List<SegmentDto>> GetSegments(Guid statorId);

    Task<string> SetSegmentCoordinates(Guid segmentId, Coordinates segmentCoordinates);
}