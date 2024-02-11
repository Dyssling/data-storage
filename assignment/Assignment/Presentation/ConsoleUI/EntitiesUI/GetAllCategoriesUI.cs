using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class GetAllCategoriesUI
    {
        private readonly ProductsDataContext _context;

        public GetAllCategoriesUI(ProductsDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            CategoryService service = new CategoryService(_context);

            var categoryList = await service.GetAllCategoriesAsync();

            foreach(CategoryDto category in categoryList)
            {
                Console.WriteLine(category.Name);
            }
        }
    }
}
