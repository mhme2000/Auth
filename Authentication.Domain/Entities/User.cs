using System.ComponentModel.DataAnnotations;

namespace Authentication.Domain.Entities
{
    public class User(string username, string email, byte[] passwordHash, byte[] passwordSalt) : Entity
    {
        [MaxLength(50)]
        public string Username { get; set; } = username;
        [MaxLength(255)]
        public string Email { get; set; } = email;
        public byte[] PasswordHash { get; set; } = passwordHash;
        public byte[] PasswordSalt { get; set; } = passwordSalt;
    }
}
