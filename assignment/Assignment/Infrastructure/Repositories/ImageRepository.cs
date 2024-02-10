using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class ImageRepository : BaseRepository<Image, ProductsDataContext>
    {
        public ImageRepository(ProductsDataContext context) : base(context)
        {
        }
    }
}

