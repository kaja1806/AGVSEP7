using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Models;
using WebAPI.Controllers;
using WebAPI.Services;
using Xunit;

namespace WebAPI.Tests;

public class SegmentControllerTests {
    [Fact]
    public async Task GetSegments_ValidStatorNo_ReturnsOkResult()
    {
        // Arrange
        var segmentServiceMock = new Mock<ISegmentService>();
        var controller = new SegmentController(segmentServiceMock.Object);

        var statorNo = 123;
        var expectedResult = new List<SegmentDto>();

        segmentServiceMock.Setup(x => x.GetSegmentsForAgv(statorNo))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await controller.GetSegments(statorNo);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<List<SegmentDto>>(okResult.Value);

        segmentServiceMock.Verify(x => x.GetSegmentsForAgv(statorNo), Times.Once);
    }

    [Fact]
    public async Task GetSegments_InvalidStatorNo_ReturnsBadRequestResult()
    {
        // Arrange
        var segmentServiceMock = new Mock<ISegmentService>();
        var controller = new SegmentController(segmentServiceMock.Object);

        int invalidStatorNo = 0;

        segmentServiceMock.Setup(x => x.GetSegmentsForAgv(invalidStatorNo))
            .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        var result = await controller.GetSegments(invalidStatorNo);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Internal Server Error + Simulated exception", badRequestResult.Value);

        segmentServiceMock.Verify(x => x.GetSegmentsForAgv(invalidStatorNo), Times.Once);
    }

    [Fact]
    public async Task SendSegmentDataToAgv_ValidSegmentNo_ReturnsOkResult()
    {
        // Arrange
        var segmentServiceMock = new Mock<ISegmentService>();
        var controller = new SegmentController(segmentServiceMock.Object);

        var segmentNo = 456;
        var expectedResult = new List<SegmentDto>();

        segmentServiceMock.Setup(x => x.GetSegmentsForAgv(segmentNo))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await controller.SendSegmentDataToAgv(segmentNo);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<List<SegmentDto>>(okResult.Value);

        segmentServiceMock.Verify(x => x.GetSegmentsForAgv(segmentNo), Times.Once);
    }

    [Fact]
    public async Task SendSegmentDataToAgv_InvalidSegmentNo_ReturnsBadRequestResult()
    {
        // Arrange
        var segmentServiceMock = new Mock<ISegmentService>();
        var controller = new SegmentController(segmentServiceMock.Object);

        int invalidSegmentNo = 0; // Replace with an invalid segment number

        segmentServiceMock.Setup(x => x.GetSegmentsForAgv(invalidSegmentNo))
            .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        var result = await controller.SendSegmentDataToAgv(invalidSegmentNo);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Internal Server Error + Simulated exception", badRequestResult.Value);

        segmentServiceMock.Verify(x => x.GetSegmentsForAgv(invalidSegmentNo), Times.Once);
    }
}