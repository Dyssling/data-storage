using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class CurrencyRepository : BaseRepository<CurrencyEntity, CustomersDataContext>
    {
        public CurrencyRepository(CustomersDataContext context) : base(context)
        {
        }
    }
}

