using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class UpdateImageUI
    {
        private readonly ProductsDataContext _context;

        public UpdateImageUI(ProductsDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            Console.Write("Ange en bild-URL vars bild du vill uppdatera: ");
            var url = Console.ReadLine()!;

            Console.Write("Ange den nya URLen: ");
            var newUrl = Console.ReadLine()!;

            ImageService service = new ImageService(_context);

            var result = await service.UpdateImageAsync(url, new ImageDto()
            {
                ImageURL = newUrl
            });

            if (result != false)
            {
                Console.WriteLine($"Bilden har uppdaterats.");
            }
            else
            {
                Console.WriteLine($"Det gick inte att uppdatera bilden.");
            }


        }
    }
}
