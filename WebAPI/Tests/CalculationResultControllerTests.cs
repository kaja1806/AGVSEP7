using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Models;
using WebAPI.Controllers;
using WebAPI.Services;
using Xunit;

namespace WebAPI.Tests;

public class CalculationResultControllerTests {
    [Fact]
    public void GetCalculationResult_ValidStatorNo_ReturnsOkResult()
    {
        // Arrange
        var calculationResultServiceMock = new Mock<ICalculationResultService>();
        var controller = new CalculationResultController(calculationResultServiceMock.Object);

        var statorNo = 123;
        var expectedResult = new List<AdjustedCalculationDto>();
        calculationResultServiceMock.Setup(x => x.GetCalculationResult(statorNo))
            .Returns(Task.FromResult(expectedResult));

        // Act
        var result = controller.GetCalculationResult(statorNo);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<List<AdjustedCalculationDto>>(okResult.Value);

        calculationResultServiceMock.Verify(x => x.GetCalculationResult(statorNo), Times.Once);
    }

    [Fact]
    public void GetCalculationResult_InvalidStatorNo_ReturnsInternalServerError()
    {
        // Arrange
        var calculationResultServiceMock = new Mock<ICalculationResultService>();
        var controller = new CalculationResultController(calculationResultServiceMock.Object);

        int? invalidStatorNo = null; // Replace with an invalid stator number

        calculationResultServiceMock.Setup(x => x.GetCalculationResult(invalidStatorNo))
            .Throws(new Exception("Simulated exception"));

        // Act
        var result = controller.GetCalculationResult(invalidStatorNo);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);

        calculationResultServiceMock.Verify(x => x.GetCalculationResult(invalidStatorNo), Times.Once);
    }

    [Fact]
    public async Task RunCalculationForSegment_ValidStatorNo_ReturnsOkResult()
    {
        // Arrange
        var calculationResultServiceMock = new Mock<ICalculationResultService>();
        var controller = new CalculationResultController(calculationResultServiceMock.Object);

        var statorNo = 123;
        var expectedResult = "Table edited successfully";

        calculationResultServiceMock.Setup(x => x.RunCalculationForSegment(statorNo))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await controller.RunCalculationForSegment(statorNo);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<string>(okResult.Value); // Replace YourResultType with the actual return type of your service

        calculationResultServiceMock.Verify(x => x.RunCalculationForSegment(statorNo), Times.Once);
    }

    [Fact]
    public async Task RunCalculationForSegment_InvalidStatorNo_ReturnsInternalServerError()
    {
        // Arrange
        var calculationResultServiceMock = new Mock<ICalculationResultService>();
        var controller = new CalculationResultController(calculationResultServiceMock.Object);

        int invalidStatorNo = 0;

        calculationResultServiceMock.Setup(x => x.RunCalculationForSegment(invalidStatorNo))
            .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        var result = await controller.RunCalculationForSegment(invalidStatorNo);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);

        calculationResultServiceMock.Verify(x => x.RunCalculationForSegment(invalidStatorNo), Times.Once);
    }
}