using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Models;
using WebAPI.Controllers;
using WebAPI.Services;
using Xunit;

namespace WebAPI.Tests;

public class AgvControllerTests {
    [Fact]
    public async Task SaveAgvStatusLogs_ValidInput_ReturnsOkResult()
    {
        // Arrange
        var agvServiceMock = new Mock<IAgvService>();
        var controller = new AgvController(agvServiceMock.Object);

        var agvStatusList = new List<AgvStatusModel>();

        agvServiceMock.Setup(x => x.SaveAgvStatusLogs(agvStatusList))
            .ReturnsAsync(new string(""));

        // Act
        var result = await controller.SaveAgvStatusLogs(agvStatusList);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<string>(okResult.Value); 
        
        agvServiceMock.Verify(x => x.SaveAgvStatusLogs(agvStatusList), Times.Once);
    }

    [Fact]
    public async Task SaveAgvStatusLogs_ServiceThrowsException_ReturnsBadRequestResult()
    {
        // Arrange
        var agvServiceMock = new Mock<IAgvService>();
        var controller = new AgvController(agvServiceMock.Object);

        var agvStatusList = new List<AgvStatusModel>
        {
            new AgvStatusModel
            {
                SegmentNo = 0,
                AddedAt = DateTime.Now,
                LogText = "Test",
                StatorNo = 0
            }
        };

        agvServiceMock.Setup(x => x.SaveAgvStatusLogs(agvStatusList))
            .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        var result = await controller.SaveAgvStatusLogs(agvStatusList);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Internal Server Error + Simulated exception", badRequestResult.Value);

        agvServiceMock.Verify(x => x.SaveAgvStatusLogs(agvStatusList), Times.Once);
    }
}