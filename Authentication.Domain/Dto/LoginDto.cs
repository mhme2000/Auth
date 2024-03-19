using System.ComponentModel.DataAnnotations;

namespace Authentication.Domain.Dto
{
    public class LoginDto
    {
        [Required]
        public string Login { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
