using System.Net.Http.Json;
using System.Text.Json;
using AGV.Models;
using Shared.Models;

namespace AGV.Services;

public class AGVSimulationService : IAGVSimulationService {
    private readonly List<AgvModel> _agvMovement = new();
    private readonly string _logFilePath = "AGVLogs.json";
    private readonly HttpClient _httpClient;
    public event EventHandler<AgvModel> StepCompleted;

    private void OnStepCompleted(AgvModel step)
    {
        StepCompleted?.Invoke(this, step);
    }

    public AGVSimulationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<SegmentDto>> GetSegmentDetails(Guid statorId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Segment/GetSegments/{statorId}");
            var palletDetails = await response.Content.ReadFromJsonAsync<List<SegmentDto>>();
            return palletDetails;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<List<AgvModel>> GetSimulatedMovements()
    {
        return Task.FromResult(_agvMovement.ToList()); // Return a copy of the list
    }

    public async Task SimulateSegmentMovement(Guid statorId)
    {
        try
        {
            var palletDetails = await GetSegmentDetails(statorId);

            // Simulate forklift movement using fetched pallet details
            foreach (var palletDetail in palletDetails)
            {
                var pickupAction = new AgvModel
                {
                    SegmentNo = palletDetail.SegmentNo,
                    Coordinates = palletDetail.SegmentCoordinates,
                    Action = "Pickup",
                    AddedAt = DateTime.Now
                };

                _agvMovement.Add(pickupAction);
                OnStepCompleted(pickupAction);
                await Task.Delay(5000); // Simulate a delay of 5 seconds

                var dropOffAction = new AgvModel
                {
                    SegmentNo = palletDetail.SegmentNo,
                    Coordinates = palletDetail.SegmentCoordinates, // You might want to adjust this based on your logic
                    Action = "DropOff",
                    AddedAt = DateTime.Now
                };

                _agvMovement.Add(dropOffAction);
                OnStepCompleted(dropOffAction);
                await Task.Delay(5000); // Simulate a delay of 5 seconds
            }

            // Serialize the list to JSON and write it to the log file
            string jsonLog = JsonSerializer.Serialize(_agvMovement);
            await File.WriteAllTextAsync(_logFilePath, jsonLog);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}