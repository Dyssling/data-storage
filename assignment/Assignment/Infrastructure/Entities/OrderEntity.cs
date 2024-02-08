using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public CustomerEntity Customer { get; set; } = null!;

        [Required]
        public string ProductList { get; set; } = null!;

        [Required]
        [Column(TypeName = "money")]
        public decimal Cost { get; set; }

        [Required]
        public string CurrencyISOCode { get; set; } = null!;
        public CurrencyEntity Currency { get; set; } = null!;
    }
}
