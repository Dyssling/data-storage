using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class GetOneCategoryUI
    {
        private readonly ProductsDataContext _context;

        public GetOneCategoryUI(ProductsDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            Console.Write("Ange kategorinamnet för kategorin du vill hämta: ");
            var name = Console.ReadLine()!;

            CategoryService service = new CategoryService(_context);

            var category = await service.GetOneCategoryAsync(name);

            if (category != null)
            {
                Console.WriteLine($"Kategorinamn: {category.Name}");
            }
            else
            {
                Console.WriteLine($"Ingen kategori hittades.");
            }
        }
    }
}
