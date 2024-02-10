using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class ProductImageRepository : BaseRepository<ProductImage, ProductsDataContext>
    {
        public ProductImageRepository(ProductsDataContext context) : base(context)
        {
        }
    }
}