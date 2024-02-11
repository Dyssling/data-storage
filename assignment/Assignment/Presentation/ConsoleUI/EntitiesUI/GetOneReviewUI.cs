using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class GetOneReviewUI
    {
        private readonly ProductsDataContext _context;

        public GetOneReviewUI(ProductsDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            Console.Write("Ange ID't för recensionen du vill hämta: ");
            var id = int.Parse(Console.ReadLine()!);

            ReviewService service = new ReviewService(_context);

            var review = await service.GetOneReviewAsync(id);

            if (review != null)
            {
                Console.WriteLine($"Kund-ID: {review.CustomerId}");
                Console.WriteLine($"Betyg: {review.Rating}/5");
                Console.WriteLine($"Meddelande: {review.Content}");
            }
            else
            {
                Console.WriteLine($"Ingen recension hittades.");
            }
        }
    }
}
