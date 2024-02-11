using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class GetOneProductUI
    {
        private readonly ProductsDataContext _context;

        public GetOneProductUI(ProductsDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            Console.Write("Ange artikelnumret för produkten du vill hämta: ");
            var articleNumber = Console.ReadLine()!;

            ProductService service = new ProductService(_context);

            var product = await service.GetOneProductAsync(articleNumber);

            if (product != null)
            {
                Console.WriteLine($"Artikelnummer: {product.ArticleNumber}");
                Console.WriteLine($"Namn: {product.Name}");
                Console.WriteLine($"Pris: {product.Price}");
            }
            else
            {
                Console.WriteLine($"Ingen produkt hittades.");
            }
        }
    }
}
