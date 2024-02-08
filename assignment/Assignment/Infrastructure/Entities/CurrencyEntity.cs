using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    [Index(nameof(Name), IsUnique = true)] //Jag vill att Name attributet ska vara unikt
    public class CurrencyEntity
    {
        [Key]
        public string ISOCode { get; set; } = null!;
        public string Name { get; set; } = null!;

        public ICollection<OrderEntity> Orders { get; set; } = new HashSet<OrderEntity>();
    }
}
