using Authentication.Domain.Entities;
using Authentication.Infrastructure.Context;
using Authentication.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Repository.Tests;

public class AuthenticationRepositoryTests
{
    private readonly AuthContext _context;
    private readonly AuthenticationRepository _authenticationRepository;

    public AuthenticationRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AuthContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new AuthContext(options);
        _authenticationRepository = new AuthenticationRepository(_context);
    }

    [Fact]
    public async Task FindByIdAsync_DeveRetornarNullQuandoUsuarioNaoEncontrado()
    {
        // Act
        var result = await _authenticationRepository.FindByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task FindByUsernameAsync_DeveRetornarUsuarioQuandoEncontrado()
    {
        // Arrange
        var username = "username";
        var userEntity = new User(username, "email", new byte[0], new byte[0]);
        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();

        // Act
        var result = await _authenticationRepository.FindByUsernameAsync(username);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(username, result!.Username);
    }

    [Fact]
    public async Task FindByUsernameAsync_DeveRetornarNullQuandoUsuarioNaoEncontrado()
    {
        // Act
        var result = await _authenticationRepository.FindByUsernameAsync("username");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_DeveAdicionarUsuario()
    {
        // Arrange
        var userEntity = new User("username", "email", new byte[0], new byte[0]);

        // Act
        await _authenticationRepository.AddAsync(userEntity);

        // Assert
        Assert.Equal(1, _context.Users.Count());
    }

    [Fact]
    public async Task UpdateAsync_DeveAtualizarUsuario()
    {
        // Arrange
        var userEntity = new User("username", "email", new byte[0], new byte[0]);
        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();

        // Act
        userEntity.Email = "updated_email";
        await _authenticationRepository.UpdateAsync(userEntity);

        // Assert
        var updatedUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == userEntity.Id);
        Assert.NotNull(updatedUser);
        Assert.Equal("updated_email", updatedUser!.Email);
    }

    [Fact]
    public async Task DeleteAsync_DeveExcluirUsuario()
    {
        // Arrange
        var userEntity = new User("username", "email", new byte[0], new byte[0]);
        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();

        // Act
        await _authenticationRepository.DeleteAsync(userEntity);

        // Assert
        var deletedUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == userEntity.Id);
        Assert.Null(deletedUser);
    }
}
