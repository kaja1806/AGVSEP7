using AGV.Models;
using Shared.Models;

namespace AGV.Services;

public interface IAGVSimulationService {

    event EventHandler<AgvModel> StepCompleted;

    Task SimulateSegmentMovement(Guid statorId);
    Task<List<AgvModel>> GetSimulatedMovements(); // Add this method
    Task<List<SegmentDto>> GetSegmentDetails(Guid statorId);

}