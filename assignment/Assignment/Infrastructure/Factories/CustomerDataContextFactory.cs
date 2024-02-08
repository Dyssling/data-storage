using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Contexts;

namespace Infrastructure.Factories
{
    public class CustomerDataContextFactory : IDesignTimeDbContextFactory<CustomersDataContext>
    {
        public CustomersDataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CustomersDataContext>();
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Education\data-storage\assignment\Assignment\Infrastructure\Data\CustomersAndOrders.mdf;Integrated Security=True;Connect Timeout=30");

            return new CustomersDataContext(optionsBuilder.Options);
        }
    }
}
