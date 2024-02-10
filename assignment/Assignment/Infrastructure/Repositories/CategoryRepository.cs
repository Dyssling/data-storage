using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category, ProductsDataContext>
    {
        public CategoryRepository(ProductsDataContext context) : base(context)
        {
        }
    }
}
