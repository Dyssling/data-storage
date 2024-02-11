using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class GetOneImageUI
    {
        private readonly ProductsDataContext _context;

        public GetOneImageUI(ProductsDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            Console.Write("Ange URLen för bilden du vill hämta: ");
            var url = Console.ReadLine()!;

            ImageService service = new ImageService(_context);

            var image = await service.GetOneImageAsync(url);

            if (image != null)
            {
                Console.WriteLine($"Bild-URL: {image.ImageURL}");
            }
            else
            {
                Console.WriteLine($"Ingen bild hittades.");
            }
        }
    }
}
