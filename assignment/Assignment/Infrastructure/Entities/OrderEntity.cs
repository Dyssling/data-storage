using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class OrderEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public CustomerEntity Customer { get; set; } = null!;

        [Required]
        public string ProductList { get; set; } = null!;

        [Required]
        public decimal Cost { get; set; }
    }
}
