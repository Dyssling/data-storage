using Business.Services;
using Business.Dtos;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class AddCategoryUI
    {
        private readonly ProductsDataContext _context;

        public AddCategoryUI(ProductsDataContext context)
        {
            _context = context;
            
        }
        public async Task Show()
        {
            Console.Write("Ange ett kategorinamn: ");
            var name = Console.ReadLine()!;

            CategoryService service = new CategoryService(_context);

            var result = await service.CreateCategoryAsync(new CategoryDto() { Name = name });

            if (result != false)
            {
                Console.WriteLine($"Kategorin har skapats.");
            }
            else
            {
                Console.WriteLine($"Det gick inte att skapa kategorin.");
            }

            
        }
    }
}
