using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class AddProductUI
    {
        private readonly ProductsDataContext _context;

        public AddProductUI(ProductsDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            Console.Write("Ange ett artikelnummer: ");
            var articleNumber = Console.ReadLine()!;

            Console.Write("Ange ett produktnamn: ");
            var name = Console.ReadLine()!;

            Console.Write("Ange ett pris: ");
            var price = decimal.Parse(Console.ReadLine()!);

            Console.Write("Ange en kategori: ");
            var categories = Console.ReadLine()!;

            Console.Write("Ange en bild-URL: ");
            var images = Console.ReadLine()!;

            Console.Write("Ange ett kund-ID för kunden som har skrivit en recension för produkten (jag vet detta är fett konstigt): ");
            var reviews = int.Parse(Console.ReadLine()!);

            ProductService service = new ProductService(_context);

            var result = await service.CreateProductAsync(new ProductDto() 
            { 
                ArticleNumber = articleNumber,
                Name = name,
                Price = price,
                Categories = new List<string>() { categories },
                Images = new List<string>() { images },
                Reviews = new List<int>() { reviews }
            });

            if (result != false)
            {
                Console.WriteLine($"Produkten har skapats.");
            }
            else
            {
                Console.WriteLine($"Det gick inte att skapa produkten.");
            }


        }
    }
}
