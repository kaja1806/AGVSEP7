using Shared.Models;

namespace WebAPI.Services;

public interface ISegmentService
{
    Task<List<SegmentModel>> GetSegments(Guid statorId);

    Task<string> SetSegmentCoordinates(Guid segmentId, Coordinates segmentCoordinates);
}