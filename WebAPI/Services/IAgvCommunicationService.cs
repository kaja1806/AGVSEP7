using Database.Models;

namespace WebAPI.Services;

internal interface IAgvCommunicationService
{
    void UpdateAgvSegments(List<SegmentModel> segmentModels);
}