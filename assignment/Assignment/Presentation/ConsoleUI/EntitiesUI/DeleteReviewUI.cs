using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class DeleteReviewUI
    {
        private readonly ProductsDataContext _context;

        public DeleteReviewUI(ProductsDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            Console.Write("Ange ID't för recensionen du vill radera: ");
            var id =int.Parse(Console.ReadLine()!);

            ReviewService service = new ReviewService(_context);

            var review = await service.DeleteReviewAsync(id);

            if (review != false)
            {
                Console.WriteLine($"Recensionen har tagits bort.");
            }
            else
            {
                Console.WriteLine($"Recensionen kunde inte tas bort.");
            }
        }
    }
}
