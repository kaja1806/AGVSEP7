using Shared.Models;

namespace WebAPI.Services;

public interface ISegmentService {
    Task<List<SegmentDto>> GetSegmentsForAgv(int statorNo);
}