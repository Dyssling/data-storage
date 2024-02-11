using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class AddImageUI
    {
        private readonly ProductsDataContext _context;

        public AddImageUI(ProductsDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            Console.Write("Ange en bild-URL: ");
            var url = Console.ReadLine()!;

            ImageService service = new ImageService(_context);

            var result = await service.CreateImageAsync(new ImageDto() 
            { 
                ImageURL = url
            });

            if (result != false)
            {
                Console.WriteLine($"Bilden har skapats.");
            }
            else
            {
                Console.WriteLine($"Det gick inte att skapa bilden.");
            }


        }
    }
}
