using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class GetAllProductsUI
    {
        private readonly ProductsDataContext _context;

        public GetAllProductsUI(ProductsDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            ProductService service = new ProductService(_context);

            var productList = await service.GetAllProductsAsync();

            foreach (ProductDto product in productList)
            {
                Console.WriteLine(product.Name);
            }
        }
    }
}
