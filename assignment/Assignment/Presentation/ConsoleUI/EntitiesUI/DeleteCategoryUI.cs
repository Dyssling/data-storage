using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class DeleteCategoryUI
    {
        private readonly ProductsDataContext _context;

        public DeleteCategoryUI(ProductsDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            Console.Write("Ange kategorinamnet för kategorin du vill radera: ");
            var name = Console.ReadLine()!;

            CategoryService service = new CategoryService(_context);

            var category = await service.DeleteCategoryAsync(name);

            if (category != false)
            {
                Console.WriteLine($"Kategorin har tagits bort.");
            }
            else
            {
                Console.WriteLine($"Kategorin kunde inte tas bort.");
            }
        }
    }
}
