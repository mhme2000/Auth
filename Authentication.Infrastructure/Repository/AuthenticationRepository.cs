using Authentication.Domain.Entities;
using Authentication.Infrastructure.Context;
using Authentication.Infrastructure.Contract;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Repository
{
    public class AuthenticationRepository(AuthContext context) : IAuthenticationRepository
    {
        private readonly AuthContext _context = context;

        public async Task<User?> FindByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<User?> FindByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(e => e.Username == username);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
