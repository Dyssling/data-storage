using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class UpdateReviewUI
    {
        private readonly ProductsDataContext _context;

        public UpdateReviewUI(ProductsDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            Console.Write("Ange ett ID vars recension du vill uppdatera: ");
            var id = int.Parse(Console.ReadLine()!);

            Console.Write("Ange det nya kund-ID't: ");
            var newCustomerId = int.Parse(Console.ReadLine()!);

            Console.Write("Ange det nya betyget mellan 1-5: ");
            var newRating = byte.Parse(Console.ReadLine()!);

            Console.Write("Ange det nya meddelandet: ");
            var newContent = Console.ReadLine()!;

            ReviewService service = new ReviewService(_context);

            var result = await service.UpdateReviewAsync(id, new ReviewDto()
            {
                CustomerId = newCustomerId,
                Rating = newRating,
                Content = newContent
            });

            if (result != false)
            {
                Console.WriteLine($"Recensionen har uppdaterats.");
            }
            else
            {
                Console.WriteLine($"Det gick inte att uppdatera recensionen.");
            }


        }
    }
}
