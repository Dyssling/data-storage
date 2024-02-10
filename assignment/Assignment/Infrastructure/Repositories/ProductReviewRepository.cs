using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class ProductReviewRepository : BaseRepository<ProductReview, ProductsDataContext>
    {
        public ProductReviewRepository(ProductsDataContext context) : base(context)
        {
        }
    }
}
