using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Models;
using WebAPI.Controllers;
using WebAPI.Services;
using Xunit;

namespace WebAPI.Tests;

public class UserControllerTests {
    [Fact]
    public async Task RegisterUser_ValidUserModelDto_ReturnsOkResult()
    {
        // Arrange
        var userServiceMock = new Mock<IUserService>();
        var controller = new UserController(userServiceMock.Object);

        var userModelDto = new UserModelDto
        {
            // Populate with test data
        };

        var expectedResult = "User Created";

        userServiceMock.Setup(x => x.CreateUser(userModelDto))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await controller.RegisterUser(userModelDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<string>(okResult.Value);

        userServiceMock.Verify(x => x.CreateUser(userModelDto), Times.Once);
    }

    [Fact]
    public async Task LoginUser_ValidCredentials_ReturnsOkResult()
    {
        // Arrange
        var userServiceMock = new Mock<IUserService>();
        var controller = new UserController(userServiceMock.Object);

        var email = "test@example.com";
        var password = "password";

        var userModelDto = new UserModelDto
        {
            Email = email,
            Password = password
        };

        var getUserResult = new UserModelDto();
        userServiceMock.Setup(x => x.GetUser(email))
            .ReturnsAsync(getUserResult);

        var expectedResult = new OkObjectResult("User created");
        userServiceMock.Setup(x => x.LoginUser(userModelDto))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await controller.LoginUser(email, password);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<OkObjectResult>(okResult);

        userServiceMock.Verify(x => x.GetUser(email), Times.Once);
    }

    [Fact]
    public async Task LogoutUser_ValidEmail_ReturnsOkResult()
    {
        // Arrange
        var userServiceMock = new Mock<IUserService>();
        var controller = new UserController(userServiceMock.Object);

        var email = "test@example.com";

        var expectedResult = new OkObjectResult("Logged out");
        userServiceMock.Setup(x => x.LogoutUser(email))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await controller.LogoutUser(email);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<OkObjectResult>(okResult.Value);

        userServiceMock.Verify(x => x.LogoutUser(email), Times.Once);
    }
}