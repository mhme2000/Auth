using Authentication.Application.Service;
using Authentication.Domain.Dto;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Contract;
using Moq;

namespace Authentication.Service.Tests;

public class AuthenticationServiceTests
{
    private readonly AuthenticationService _authenticationService;
    private readonly Mock<IAuthenticationRepository> _authenticationRepositoryMock = new Mock<IAuthenticationRepository>();

    public AuthenticationServiceTests()
    {
        _authenticationService = new AuthenticationService(_authenticationRepositoryMock.Object);
    }

    [Fact]
    public async Task FindAsync_DeveRetornarUsuarioQuandoEncontrado()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userEntity = new User("username", "email", new byte[0], new byte[0]);
        _authenticationRepositoryMock.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync(userEntity);

        // Act
        var result = await _authenticationService.FindAsync(userId);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task FindAsync_DeveRetornarNullQuandoUsuarioNaoEncontrado()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _authenticationRepositoryMock.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync((User)null);

        // Act
        var result = await _authenticationService.FindAsync(userId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_DeveRetornarIdQuandoUsuarioAdicionado()
    {
        // Arrange
        var userDto = new AddOrUpdateUserDto { Username = "username", Email = "email", Password = "password" };

        // Act
        var result = await _authenticationService.AddAsync(userDto);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateAsync_DeveRetornarUsuarioQuandoAtualizado()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userDto = new AddOrUpdateUserDto { Username = "username", Email = "email", Password = "password" };
        var userEntity = new User("old_username", "old_email", new byte[0], new byte[0]);
        _authenticationRepositoryMock.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync(userEntity);

        // Act
        var result = await _authenticationService.UpdateAsync(userDto, userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userDto.Username, result!.Username);
        Assert.Equal(userDto.Email, result!.Email);
    }

    [Fact]
    public async Task UpdateAsync_DeveRetornarNullQuandoUsuarioNaoEncontrado()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userDto = new AddOrUpdateUserDto { Username = "username", Email = "email", Password = "password" };
        _authenticationRepositoryMock.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync((User)null);

        // Act
        var result = await _authenticationService.UpdateAsync(userDto, userId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteAsync_DeveRetornarIdQuandoUsuarioExcluido()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userEntity = new User("username", "email", new byte[0], new byte[0]);
        _authenticationRepositoryMock.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync(userEntity);

        // Act
        var result = await _authenticationService.DeleteAsync(userId);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task DeleteAsync_DeveRetornarNullQuandoUsuarioNaoEncontrado()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _authenticationRepositoryMock.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync((User)null);

        // Act
        var result = await _authenticationService.DeleteAsync(userId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task LoginAsync_DeveRetornarNullQuandoUsuarioNaoEncontrado()
    {
        // Arrange
        var loginDto = new LoginDto { Login = "username", Password = "password" };
        _authenticationRepositoryMock.Setup(x => x.FindByUsernameAsync(loginDto.Login)).ReturnsAsync((User)null);

        // Act
        var result = await _authenticationService.LoginAsync(loginDto);

        // Assert
        Assert.Null(result);
    }
}
