using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class UpdateCategoryUI
    {
        private readonly ProductsDataContext _context;

        public UpdateCategoryUI(ProductsDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            Console.Write("Ange ett kategorinamn vars kategori du vill uppdatera: ");
            var name = Console.ReadLine()!;

            Console.Write("Ange det nya kategorinamnet: ");
            var newName = Console.ReadLine()!;

            CategoryService service = new CategoryService(_context);

            var result = await service.UpdateCategoryAsync(name, new CategoryDto()
            {
                Name = newName
            });

            if (result != false)
            {
                Console.WriteLine($"Kategorin har uppdaterats.");
            }
            else
            {
                Console.WriteLine($"Det gick inte att uppdatera kategorin.");
            }


        }
    }
}
