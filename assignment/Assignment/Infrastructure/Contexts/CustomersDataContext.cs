using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
    public partial class CustomersDataContext : DbContext
    {
        public CustomersDataContext(DbContextOptions<CustomersDataContext> options) : base(options)
        {
        }

        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<CurrencyEntity> Currencies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrencyEntity>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.Currency)
                .OnDelete(DeleteBehavior.NoAction); //Jag vill inte att det eventuellt ska bli en Cascade när man tar bort en currency som används i en order som redan är lagd
        }
    }
}
