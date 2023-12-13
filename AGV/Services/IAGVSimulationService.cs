using Shared.Models;

namespace AGV.Services;

public interface IAGVSimulationService {

    event EventHandler<AgvStatusModel> StepCompleted;

    Task SimulateSegmentMovement(int statorNo);
    Task<string> SendToDatabase(List<AgvStatusModel> simulatedMovements);
}