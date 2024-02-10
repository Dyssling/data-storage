using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product, ProductsDataContext>
    {
        public ProductRepository(ProductsDataContext context) : base(context)
        {
        }
    }
}

