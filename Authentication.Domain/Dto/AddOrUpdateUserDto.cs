using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Domain.Dto
{
    public class AddOrUpdateUserDto
    {
        [Required]
        [MaxLength(50)]
        [MinLength(10)]
        public string Username { get; set; } = null!;
        [EmailAddress]
        [Required]
        public string Email { get; set; } = null!;
        [PasswordPropertyText]
        [Required]
        public string Password { get; set; } = null!;
    }
}

