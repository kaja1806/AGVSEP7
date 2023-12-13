using System.Net.Http.Json;
using Shared.Models;

namespace AGV.Services;

public class AgvSimulationService : IAGVSimulationService
{
    private readonly List<AgvStatusModel> _agvMovement = new();
    private readonly HttpClient _httpClient;
    public event EventHandler<AgvStatusModel>? StepCompleted;

    private void OnStepCompleted(AgvStatusModel step)
    {
        StepCompleted?.Invoke(this, step);
    }

    public AgvSimulationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private async Task<List<SegmentDto>> GetSegmentDetails(int statorNo)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Segment/GetSegments/{statorNo}");
            var segmentDetails = await response.Content.ReadFromJsonAsync<List<SegmentDto>>();
            return segmentDetails;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<string> SendToDatabase(List<AgvStatusModel> simulatedMovements)
    {
        try
        {
            var result = await _httpClient.PostAsJsonAsync($"api/Agv/SaveAgvStatusLogs", simulatedMovements) ??
                         throw new InvalidOperationException();
            var jsonResponse = await result.Content.ReadAsStringAsync();
            return jsonResponse;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task SimulateSegmentMovement(int statorNo)
    {
        try
        {
            var segmentDetails = await GetSegmentDetails(statorNo);

            foreach (var segmentDetail in segmentDetails)
            {
                var pickupAction = new AgvStatusModel
                {
                    SegmentNo = segmentDetail.SegmentNo,
                    LogText =
                        $"Pickup at X:{segmentDetail.SegmentCoordinates.LocationX} , Y:{segmentDetail.SegmentCoordinates.LocationY}",
                    AddedAt = DateTime.Now, // simulation, AGV would send a timestamp when it gets to somewhere
                    StatorNo = statorNo
                };

                _agvMovement.Add(pickupAction);
                OnStepCompleted(pickupAction);
                await Task.Delay(100);

                var inTransportAction = new AgvStatusModel
                {
                    SegmentNo = segmentDetail.SegmentNo,
                    LogText = "Transporting to stator",
                    AddedAt = DateTime.Now,
                    StatorNo = statorNo
                };

                _agvMovement.Add(inTransportAction);
                OnStepCompleted(inTransportAction);
                await Task.Delay(200);

                var inPositionAction = new AgvStatusModel
                {
                    SegmentNo = segmentDetail.SegmentNo,
                    LogText = "In position at stator",
                    AddedAt = DateTime.Now,
                    StatorNo = statorNo
                };

                _agvMovement.Add(inPositionAction);
                OnStepCompleted(inPositionAction);
                await Task.Delay(100);

                var installingAction = new AgvStatusModel
                {
                    SegmentNo = segmentDetail.SegmentNo,
                    LogText = "Installing...",
                    AddedAt = DateTime.Now,
                    StatorNo = statorNo
                };

                _agvMovement.Add(installingAction);
                OnStepCompleted(installingAction);
                await Task.Delay(200);

                var installCompletedAction = new AgvStatusModel
                {
                    SegmentNo = segmentDetail.SegmentNo,
                    LogText = "Install completed",
                    AddedAt = DateTime.Now,
                    StatorNo = statorNo
                };

                _agvMovement.Add(installCompletedAction);
                OnStepCompleted(installCompletedAction);
                await Task.Delay(100);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}