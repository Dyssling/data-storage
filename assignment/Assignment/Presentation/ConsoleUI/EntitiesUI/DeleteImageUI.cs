using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class DeleteImageUI
    {
        private readonly ProductsDataContext _context;

        public DeleteImageUI(ProductsDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            Console.Write("Ange URLen för bilden du vill radera: ");
            var url = Console.ReadLine()!;

            ImageService service = new ImageService(_context);

            var image = await service.DeleteImageAsync(url);

            if (image != false)
            {
                Console.WriteLine($"Bilden har tagits bort.");
            }
            else
            {
                Console.WriteLine($"Bilden kunde inte tas bort.");
            }
        }
    }
}
