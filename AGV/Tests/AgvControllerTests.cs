using AGV.Controllers;
using AGV.Models;
using Shared.Models;
using Xunit;

namespace AGV.Tests;

public class AgvControllerTests
{
    [Fact]
    public async Task TestAgvFunctionality()
    {
        // Arrange
        AgvController agvController = new AgvController();
        string jsonFilePath = "agvLogTest.json";

        var agvModelMock = new AgvModel
        {
            Coordinates = new Coordinates
            {
                LocationX = 25,
                LocationY = 45,
                LocationZ = 11
            },
            SegmentNo = 1
        };

        // Act
        agvController.AssignCoordinates(agvModelMock);
        await agvController.StartAgv();

        agvController.SaveLogToJson(jsonFilePath, Guid.NewGuid());

        var deserializedLog = agvController.ReadLogFromJson(jsonFilePath);

        // Check if the JSON file is not empty
        string jsonContent = File.ReadAllText(jsonFilePath);
        Assert.False(string.IsNullOrEmpty(jsonContent));

        // Deserialize the JSON content to ensure it's valid JSON
        Assert.NotNull(deserializedLog);
        
        Assert.NotNull(deserializedLog.Segment.First().Actions.First());
    }
}