using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class DeleteProductUI
    {
        private readonly ProductsDataContext _context;

        public DeleteProductUI(ProductsDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            Console.Write("Ange artikelnumret för produkten du vill radera: ");
            var articleNumber = Console.ReadLine()!;

            ProductService service = new ProductService(_context);

            var product = await service.DeleteProductAsync(articleNumber);

            if (product != false)
            {
                Console.WriteLine($"Produkten har tagits bort.");
            }
            else
            {
                Console.WriteLine($"Produkten kunde inte tas bort.");
            }
        }
    }
}
