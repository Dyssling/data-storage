using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class ReviewRepository : BaseRepository<Review, ProductsDataContext>
    {
        public ReviewRepository(ProductsDataContext context) : base(context)
        {
        }
    }
}
