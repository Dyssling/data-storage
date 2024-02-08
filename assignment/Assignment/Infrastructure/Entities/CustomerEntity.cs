using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class CustomerEntity
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        public ICollection<OrderEntity> Orders { get; set; } = new HashSet<OrderEntity>();
    }
}
