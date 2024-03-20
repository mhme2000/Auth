using Authentication.API.Controllers;
using Authentication.Application.Contract;
using Authentication.Domain.Dto;
using Authentication.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Authentication.API.Tests;

public class AuthenticationControllerTests
{
    private readonly AuthenticationController _controller;
    private readonly Mock<IAuthenticationService> _authenticationServiceMock = new();

    public AuthenticationControllerTests()
    {
        _controller = new AuthenticationController(_authenticationServiceMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_DeveRetornarOkQuandoUsuarioEncontrado()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var entity = new UserDto (new User("Novo Usuário", "novousuario@teste.com", null, null));
        _authenticationServiceMock.Setup(x => x.FindAsync(userId)).ReturnsAsync(entity);

        // Act
        var result = await _controller.GetByIdAsync(userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(entity, okResult.Value);
    }

    [Fact]
    public async Task GetByIdAsync_DeveRetornarNotFoundQuandoUsuarioNaoEncontrado()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _authenticationServiceMock.Setup(x => x.FindAsync(userId)).ReturnsAsync((UserDto)null);

        // Act
        var result = await _controller.GetByIdAsync(userId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task PostAsync_DeveRetornarCreated()
    {
        // Arrange
        var request = new AddOrUpdateUserDto { Username = "Novo Usuário", Email = "novousuario@teste.com", Password = "Passwork" };
        _authenticationServiceMock.Setup(x => x.AddAsync(request)).ReturnsAsync(Guid.NewGuid());

        // Act
        var result = await _controller.PostAsync(request);

        // Assert
        Assert.IsType<CreatedResult>(result);
    }
}