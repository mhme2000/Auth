using System.ComponentModel.DataAnnotations;

namespace Authentication.Domain.Entities
{
    public class Entity
    {
        [Key]
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }

        public void Delete()
        {
            DeletedDate = DateTime.Now;
            IsDeleted = true;
        }
    }
}
