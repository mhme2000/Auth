using Authentication.Domain.Entities;
using System.Text.Json.Serialization;

namespace Authentication.Domain.Dto
{
    public class UserDto(User user)
    {
        public Guid Registration { get; set; } = user.Id;
        public string Username { get; set; } = user.Username;
        public string Email { get; set; } = user.Email;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? IsDeleted { get; set; } = user.IsDeleted;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? DeletedDate { get; set; } = user.DeletedDate;
    }
}
