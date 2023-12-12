using Shared.Models;

namespace AGV.Services;

public interface IAGVSimulationService {

    event EventHandler<AgvStatusModel> StepCompleted;

    Task SimulateSegmentMovement(Guid statorId);
    Task<List<AgvStatusModel>> GetSimulatedMovements(); // Add this method
    Task<List<SegmentDto>> GetSegmentDetails(Guid statorId);

}