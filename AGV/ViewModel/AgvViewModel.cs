using AGV.Controllers;
using AGV.Models;
using AGV.Services;
using Shared.Models;

namespace AGV.ViewModel;

public class AgvViewModel
{
    private AgvController _agvController;
    private ApiClient _apiClient;

    public List<AgvStatusModel> AgvLog { get; set; }

    public AgvViewModel(AgvController agvController, ApiClient apiClient, List<AgvStatusModel> agvLog)
    {
        _agvController = agvController;
        _apiClient = apiClient;
        AgvLog = agvLog;
    }

    public async Task StartForkliftAsync()
    {
        // Get coordinates and pallet ID from the external API
        var responseFromAPI = await _apiClient.GetSegmentCoordinates();

        // Assign coordinates and pallet ID to the forklift
        /*_agvController.AssignCoordinates(responseFromAPI.Coordinates.LocationX,
            responseFromAPI.Coordinates.LocationY,
            responseFromAPI.Coordinates.LocationZ, responseFromAPI.SegmentNo);*/
        // Need to change

        // Start the forklift
        await _agvController.StartAgv();

        // Update the UI with the forklift log
        AgvLog = _agvController.GetAgvLog();
    }
}