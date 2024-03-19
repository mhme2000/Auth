using Authentication.Domain.Entities;

namespace Authentication.Infrastructure.Contract
{
    public interface IAuthenticationRepository
    {
        Task<User?> FindByIdAsync(Guid id);
        Task<User?> FindByUsernameAsync(string username);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}
