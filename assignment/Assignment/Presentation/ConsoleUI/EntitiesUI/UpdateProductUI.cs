using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class UpdateProductUI
    {
        private readonly ProductsDataContext _context;

        public UpdateProductUI(ProductsDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            Console.Write("Ange ett artikelnummer vars produkt du vill uppdatera: ");
            var articleNumber = Console.ReadLine()!;

            Console.Write("Ange det nya produktnamnet: ");
            var newName = Console.ReadLine()!;

            Console.Write("Ange det nya priset: ");
            var newPrice = decimal.Parse(Console.ReadLine()!);

            ProductService service = new ProductService(_context);

            var result = await service.UpdateProductAsync(articleNumber, new ProductDto()
            {
                Name = newName,
                Price = newPrice
            });

            if (result != false)
            {
                Console.WriteLine($"Produkten har uppdaterats.");
            }
            else
            {
                Console.WriteLine($"Det gick inte att uppdatera produkten.");
            }


        }
    }
}
