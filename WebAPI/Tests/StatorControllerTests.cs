using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Models;
using WebAPI.Controllers;
using WebAPI.Services;
using Xunit;

namespace WebAPI.Tests;

public class StatorControllerTests {
    [Fact]
    public async Task GetStator_ValidData_ReturnsOkResult()
    {
        // Arrange
        var statorServiceMock = new Mock<IStatorService>();
        var controller = new StatorController(statorServiceMock.Object);

        var expectedResult = new List<StatorDto>();

        statorServiceMock.Setup(x => x.GetStator())
            .ReturnsAsync(expectedResult);

        // Act
        var result = await controller.GetStator();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<List<StatorDto>>(okResult.Value);

        statorServiceMock.Verify(x => x.GetStator(), Times.Once);
    }

    [Fact]
    public async Task SetStatorFinished_ValidStatorNo_ReturnsOkResult()
    {
        // Arrange
        var statorServiceMock = new Mock<IStatorService>();
        var controller = new StatorController(statorServiceMock.Object);

        var statorNo = 123;
        var expectedResult = "Stator Added";

        statorServiceMock.Setup(x => x.SetStatorFinished(statorNo))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await controller.SetStatorFinished(statorNo);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<string>(okResult.Value);

        statorServiceMock.Verify(x => x.SetStatorFinished(statorNo), Times.Once);
    }

    [Fact]
    public async Task SetStator_ValidStatorDto_ReturnsOkResult()
    {
        // Arrange
        var statorServiceMock = new Mock<IStatorService>();
        var controller = new StatorController(statorServiceMock.Object);

        var statorDto = new StatorDto();

        var existingStators = new List<StatorDto>();

        statorServiceMock.Setup(x => x.GetStator())
            .ReturnsAsync(existingStators);

        statorServiceMock.Setup(x => x.SetNewStator(statorDto))
            .ReturnsAsync(true);

        // Act
        var result = await controller.SetStator(statorDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal($"Stator {statorDto.StatorNo} created successfully", okResult.Value);

        statorServiceMock.Verify(x => x.GetStator(), Times.Once);
        statorServiceMock.Verify(x => x.SetNewStator(statorDto), Times.Once);
    }

    [Fact]
    public async Task SetStator_ExistingStatorNo_ReturnsBadRequestResult()
    {
        // Arrange
        var statorServiceMock = new Mock<IStatorService>();
        var controller = new StatorController(statorServiceMock.Object);

        var statorDto = new StatorDto
        {
            // Populate with test data, including an existing stator number
        };

        var existingStators = new List<StatorDto>
        {
            // Populate with existing stators, including one with the same number as statorDto
            new StatorDto() { StatorNo = statorDto.StatorNo }
        };

        statorServiceMock.Setup(x => x.GetStator())
            .ReturnsAsync(existingStators);

        // Act
        var result = await controller.SetStator(statorDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal($"Stator {statorDto.StatorNo} already exists", badRequestResult.Value);

        statorServiceMock.Verify(x => x.GetStator(), Times.Once);
        statorServiceMock.Verify(x => x.SetNewStator(It.IsAny<StatorDto>()), Times.Never);
    }

    [Fact]
    public async Task SetStator_FailedToCreateStator_ReturnsBadRequestResult()
    {
        // Arrange
        var statorServiceMock = new Mock<IStatorService>();
        var controller = new StatorController(statorServiceMock.Object);

        var statorDto = new StatorDto();

        var existingStators = new List<StatorDto>();

        statorServiceMock.Setup(x => x.GetStator())
            .ReturnsAsync(existingStators);

        statorServiceMock.Setup(x => x.SetNewStator(statorDto))
            .ReturnsAsync(false); // Indicate failure for simplicity

        // Act
        var result = await controller.SetStator(statorDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Failed to create Stator", badRequestResult.Value);

        statorServiceMock.Verify(x => x.GetStator(), Times.Once);
        statorServiceMock.Verify(x => x.SetNewStator(statorDto), Times.Once);
    }
}