using AGV.Helpers;
using AGV.Models;
using AGV.Services;
using Shared.Models;
using static Shared.Enums.StatusEnums;

namespace AGV.Controllers;

public class AgvController
{
    private readonly List<AgvStatusModel> _log = new();
    private AgvModel _currentAgvState = new();
    private readonly IApiClient _apiClient;

    public event EventHandler<AgvLogEventArgs> AgvLogUpdated;

    public void AssignCoordinates(double x, double y, double z, int segmentNo)
    {
        _currentAgvState.Coordinates.LocationX = x;
        _currentAgvState.Coordinates.LocationY = y;
        _currentAgvState.Coordinates.LocationZ = z;
        _currentAgvState.SegmentNo = segmentNo;
    }

    public void AssignCoordinates(AgvModel agvModel)
    {
        _currentAgvState = agvModel;
    }

    public async Task StartAgv()
    {
        // Start the AGV
        LogAction($"AGV started with coordinates: X={_currentAgvState.Coordinates.LocationX}, " +
                  $"Y={_currentAgvState.Coordinates.LocationY}, " +
                  $"Z={_currentAgvState.Coordinates.LocationZ}, " +
                  $"Segment ID={_currentAgvState.SegmentNo}", SegmentStatus.Waiting);

        // Simulate AGV actions
        await MoveToPosition(_currentAgvState);
        // ... (other actions)
    }

    private void LogAction(string action, SegmentStatus status)
    {
        var logEntry = new AgvStatusModel
        {
            Timestamp = DateTime.Now,
            LogText = action,
            Status = status
        };
        _log.Add(logEntry);
    }

    public List<AgvStatusModel> GetAgvLog()
    {
        return _log;
    }

    public void InstallingSegment()
    {
        LogAction("Picking up segment", SegmentStatus.Installation);
        // Logic for Installing Segment
    }

    public async Task MoveToPosition(AgvModel position)
    {
        LogAction($"Moving to segment: {position.SegmentNo}", SegmentStatus.Transport);
    }

    public void SaveLogToJson(string filePath, Guid statorId)
    {
        var agvJsonModel = new AgvJsonFormatModel()
        {
            StatorID = statorId,
            Segment = new List<SegmentFormatModel>
            {
                new()
                {
                    SegmentId = _currentAgvState.SegmentNo,
                    Actions = _log,
                }
            }
        };

        // Serialize the entire list of segments to JSON
        string json = JsonHelper.SerializeObject(agvJsonModel);

        // Write the JSON to the file
        File.WriteAllText(filePath, json);
    }

    public AgvJsonFormatModel ReadLogFromJson(string filePath)
    {
        if (!File.Exists(filePath))
            return new AgvJsonFormatModel();
        string json = File.ReadAllText(filePath);
        return JsonHelper.DeserializeObject<AgvJsonFormatModel>(json);
    }

    //This needs to be used in UI to have real time data in a table of what the AGV is doing
    public void SendLogToUIRealTimeData(AgvJsonFormatModel agvJsonFormatModel)
    {
        // Notify UI in real-time
        AgvLogUpdated?.Invoke(this, new AgvLogEventArgs(agvJsonFormatModel));
    }

    public async Task SendCompletedLogToWebApi(string statorId)
    {
        // Serialize the entire list of logs to JSON
        string json = JsonHelper.SerializeObject(_log);

        // Send the log to the WebAPI for storage in the database
        await _apiClient.SendLogToWebApi(statorId, json);

        // Optionally, you can clear the log after saving
        _log.Clear();
    }
}