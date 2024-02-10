using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<OrderEntity, CustomersDataContext>
    {
        public OrderRepository(CustomersDataContext context) : base(context)
        {
        }
    }
}
