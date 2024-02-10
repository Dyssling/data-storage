using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : BaseRepository<CustomerEntity, CustomersDataContext>
    {
        public CustomerRepository(CustomersDataContext context) : base(context)
        {
        }
    }
}
